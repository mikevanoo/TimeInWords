namespace TextToTimeGridLib.Tests;

/*
 * TestTimeGrid raw layout (row : text):
 *   0: ITLISLSTIME
 *   1: ACQUARTERDC
 *   2: TWENTYFIVEX
 *   3: HALFBTENFTO
 *   4: PASTERUNINE
 *   5: ONESIXTHREE
 *   6: FOURFIVETWO
 *   7: EIGHTELEVEN
 *   8: SEVENTWELVE
 *   9: TENSEOCLOCK
 */
public class BitmaskAlgorithmShould
{
    private readonly TestTimeGrid _grid = new();

    [Fact]
    public void ReturnEmptyBitmaskForEmptyInputInStrictMode()
    {
        var bitmask = _grid.GetBitMask("", strict: true);

        AllCells(bitmask).Should().AllSatisfy(v => v.Should().BeFalse());
    }

    [Fact]
    public void ReturnEmptyBitmaskForEmptyInputInNonStrictMode()
    {
        var bitmask = _grid.GetBitMask("", strict: false);

        AllCells(bitmask).Should().AllSatisfy(v => v.Should().BeFalse());
    }

    [Fact]
    public void LightUpASingleWordInStrictMode()
    {
        var bitmask = _grid.GetBitMask("TIME", strict: true);

        // TIME occupies row 0 columns 7-10.
        bitmask.Mask[0][7].Should().BeTrue();
        bitmask.Mask[0][8].Should().BeTrue();
        bitmask.Mask[0][9].Should().BeTrue();
        bitmask.Mask[0][10].Should().BeTrue();
        CountLit(bitmask).Should().Be(4);
    }

    [Fact]
    public void LightUpEveryWordOfAMultiWordPhraseInStrictMode()
    {
        var bitmask = _grid.GetBitMask("IT IS TIME", strict: true);

        // Row 0 = "ITLISLSTIME": IT at 0-1, IS at 3-4, TIME at 7-10.
        bitmask.Mask[0][0].Should().BeTrue();
        bitmask.Mask[0][1].Should().BeTrue();
        bitmask.Mask[0][3].Should().BeTrue();
        bitmask.Mask[0][4].Should().BeTrue();
        bitmask.Mask[0][7].Should().BeTrue();
        bitmask.Mask[0][10].Should().BeTrue();
        CountLit(bitmask).Should().Be(8);
    }

    [Fact]
    public void RespectWordBoundariesInStrictMode()
    {
        // Searching for "IT" alone must not also light the 'T' in "TIME" that
        // appears later on the same row — strict mode stops once the word is
        // complete and requires full-word boundaries.
        var bitmask = _grid.GetBitMask("IT", strict: true);

        bitmask.Mask[0][0].Should().BeTrue();
        bitmask.Mask[0][1].Should().BeTrue();
        bitmask.Mask[0][7].Should().BeFalse();
        CountLit(bitmask).Should().Be(2);
    }

    [Fact]
    public void RecoverFromAdjacentDuplicateLettersDuringStrictSearch()
    {
        // Row 5 "ONESIXTHREE" ends in "EE". Searching for "ELEVEN" must walk
        // past that duplicate and still locate ELEVEN on row 7. This exercises
        // the HandleDuplicateCharacters reset branch.
        var bitmask = _grid.GetBitMask("ELEVEN", strict: true);

        // ELEVEN is row 7 columns 5-10.
        for (var col = 5; col <= 10; col++)
        {
            bitmask.Mask[7][col].Should().BeTrue();
        }
        CountLit(bitmask).Should().Be(6);
    }

    [Fact]
    public void StopScanningAfterAllWordsAreFoundInStrictMode()
    {
        // "IT IS" is fully satisfied on row 0. No cells on later rows should
        // be lit — the scan must terminate rather than continue matching.
        var bitmask = _grid.GetBitMask("IT IS", strict: true);

        for (var row = 1; row < _grid.GridHeight; row++)
        {
            bitmask.Mask[row].Should().AllSatisfy(v => v.Should().BeFalse());
        }
    }

    [Fact]
    public void KeepEarlierMatchesEvenWhenALaterWordIsMissingInStrictMode()
    {
        // "IT" and "IS" exist on row 0; "XYZZY" does not exist anywhere.
        // The algorithm must preserve the found words rather than rolling back.
        var bitmask = _grid.GetBitMask("IT IS XYZZY", strict: true);

        bitmask.Mask[0][0].Should().BeTrue();
        bitmask.Mask[0][1].Should().BeTrue();
        bitmask.Mask[0][3].Should().BeTrue();
        bitmask.Mask[0][4].Should().BeTrue();
        CountLit(bitmask).Should().Be(4);
    }

    [Fact]
    public void ReturnEmptyBitmaskWhenNoWordCanBeFoundInStrictMode()
    {
        var bitmask = _grid.GetBitMask("XYZZY", strict: true);

        AllCells(bitmask).Should().AllSatisfy(v => v.Should().BeFalse());
    }

    [Fact]
    public void ConsumeInputCharactersInOrderAcrossTheGridInNonStrictMode()
    {
        // Non-strict ignores word boundaries and spaces. "IT IS" becomes
        // the character stream I, T, I, S consumed left-to-right, top-to-bottom
        // across row 0 "ITLISLSTIME".
        var bitmask = _grid.GetBitMask("IT IS", strict: false);

        bitmask.Mask[0][0].Should().BeTrue(); // I
        bitmask.Mask[0][1].Should().BeTrue(); // T
        bitmask.Mask[0][3].Should().BeTrue(); // I (skips L at col 2)
        bitmask.Mask[0][4].Should().BeTrue(); // S
        CountLit(bitmask).Should().Be(4);
    }

    [Fact]
    public void PullCharactersFromAnywhereInTheGridInNonStrictMode()
    {
        // Characters need not be contiguous. "OT" finds the first 'O' then
        // the first 'T' that appears after it in reading order.
        var bitmask = _grid.GetBitMask("OT", strict: false);

        // First 'O' is row 3 col 10 (end of "HALFBTENFTO").
        // Next 'T' after that is row 4 col 3 (in "PASTERUNINE").
        bitmask.Mask[3][10].Should().BeTrue();
        bitmask.Mask[4][3].Should().BeTrue();
        CountLit(bitmask).Should().Be(2);
    }

    [Fact]
    public void StopAfterAllInputCharactersAreConsumedInNonStrictMode()
    {
        var bitmask = _grid.GetBitMask("I", strict: false);

        bitmask.Mask[0][0].Should().BeTrue();
        CountLit(bitmask).Should().Be(1);
    }

    private static IEnumerable<bool> AllCells(Bitmask bitmask) =>
        bitmask.Mask.SelectMany(row => row);

    private static int CountLit(Bitmask bitmask) =>
        AllCells(bitmask).Count(v => v);
}
