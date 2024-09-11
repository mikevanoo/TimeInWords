# Overview

A [QLOCKTWO](https://www.qlocktwo.com/en-de/earth/90/black-pepper)-inspired full-screen/screensaver application,
adapting portions from [https://github.com/sedrubal/WordClockScr](https://github.com/sedrubal/WordClockScr).

Uses and includes a multilingual implementation of a word clock algorithm that converts a timestamp to its text 
representation and then to a bit-mask for an LED grid. Currently supports English and Dutch. Includes a fork the 
original work from [https://github.com/TheBauwssss/TimeInWords](https://github.com/TheBauwssss/TimeInWords) with enhancements and additions.


Demonstrates:
- (TBC)


## Full-screen/Screensaver Application

**(further details are TBC)**

<img src="images/time-in-words-screenshot.png" alt="word clock screenshot" style="border: 1px solid darkgray;" width="50%" height="50%">

# Word Clock Algorithm
![alt text](https://raw.githubusercontent.com/TheBauwssss/TimeInWords/master/images/window.png "Debug Window Preview")

## Using the time-to-text functionality

###### Code
```c#
LanguagePreset.Language lang = LanguagePreset.Language.English;
string text = TimeToText.GetSimple(lang, DateTime.Now);
```

###### Output
```
IT IS A QUARTER PAST TEN
```

## Using the text-to-grid functionality

### Displaying a time string

###### Code
```c#
TimeGrid grid = new TimeGridEnglish();
Bitmask bitmask = grid.GetBitMask("IT IS A QUARTER PAST TEN", true); //true: only accept exact word matches
string result = grid.ToString(bitmask);
```

###### Output
```
grid            bitmask         result

ITLISLSTIME     11011000000     IT.IS......
ACQUARTERDC     10111111100     A.QUARTER..
TWENTYFIVEX     00000000000     ...........
HALFBTENFTO     00000000000     ...........
PASTERUNINE     11110000000     PAST.......
ONESIXTHREE     00000000000     ...........
FOURFIVETWO     00000000000     ...........
EIGHTELEVEN     00000000000     ...........
SEVENTWELVE     00000000000     ...........
TENSEOCLOCK     11100000000     TEN........
```

### Displaying a generic string

###### Code
```c#
TimeGrid grid = new TimeGridEnglish();
Bitmask bitmask = grid.GetBitMask("HELLO", false); //false: accept partial matches
string result = grid.ToString(bitmask);
```

###### Output
```
grid            bitmask         result

ITLISLSTIME     00000000000     ...........
ACQUARTERDC     00000000000     ...........
TWENTYFIVEX     00000000000     ...........
HALFBTENFTO     10000010000     H.....E....
PASTERUNINE     00000000000     ...........
ONESIXTHREE     00000000000     ...........
FOURFIVETWO     00000000000     ...........
EIGHTELEVEN     00000010000     ......L....
SEVENTWELVE     00000000100     ........L..
TENSEOCLOCK     00000100000     .....O.....
```
