# FingerTape: Biometric Identification Desktop App

> **Tugas Besar 3 IF2211 Strategi Algoritma**

> FingerTape is a robust and user-friendly desktop application designed for individual biometric identification using fingerprint images. Developed with C# and Visual Studio .NET, FingerTape leverages advanced pattern matching algorithms, including Boyer-Moore and Knuth-Morris-Pratt, to accurately and efficiently match fingerprint patterns against a comprehensive database.

\_\_ <img src="./fingertape.jpg" alt="drawing" width="200"/>

> **Knuth Morris Pratt Algorithm**

> The KMP algorithm is an efficient string matching technique designed to find occurrences of a "pattern" string within a "text" string. It preprocesses the pattern to create a partial match table (also known as the "failure function"), which stores the lengths of the longest prefixes that are also suffixes. This preprocessing allows the algorithm to skip unnecessary comparisons, thus improving the search time.

> **Boyer Moore Algorithm**

> The Boyer-Moore algorithm is another efficient string matching technique, known for its practical performance on typical text. It preprocesses the pattern to create two arrays: the bad character table and the good suffix table. These tables help the algorithm to skip sections of the text that cannot match the pattern, by moving the pattern in larger increments compared to a straightforward character-by-character search.

## Table of Contents

- [Technologies Used](#technologies-used)
- [Pre Requirement](#pre-requirement)
- [How To Run](#how-to-run)
- [Authors](#authors)

## Technologies Used

- C#
- Avalonia
- .NET

## Pre Requirement

- .NET 8.0 or higher installed

## How To Run

Clone the project using the following command:

```bash
git clone https://github.com/caernations/Tubes3_TheTorturedInformaticsDepartment.git
```

Navigate to the project directory using the following command:

```bash
cd src/GUI
```

Build the project using the following command:

```bash
dotnet build
```

Run the project using the following command:

```bash
dotnet run
```

## Authors

Created by

- [@satriadhikara](https://github.com/satriadhikara)
- [@caernations](https://github.com/caernations)
- [@fnathas](https://github.com/fnathas)
