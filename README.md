# FingerTape: Biometric Identification Desktop App

> **Tugas Besar 3 IF2211 Strategi Algoritma**

> FingerTape is a robust and user-friendly desktop application designed for individual biometric identification using fingerprint images. Developed with C# and Visual Studio .NET, FingerTape leverages advanced pattern matching algorithms, including Boyer-Moore and Knuth-Morris-Pratt, to accurately and efficiently match fingerprint patterns against a comprehensive database.


__ <img src="./fingertape.jpg" alt="drawing" width="200"/>


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
- dotnet
-

## Pre Requirement

- dotnet

## How To Run

Run both of the frontend and backend

Then go to [LookAt](http://localhost:3000/)

### Frontend

Go to 'frontend' directory

```bash
cd frontend
```

#### Install Dependencies

Install the required dependencies

```bash
npm install
```

#### Run

```bash
npm run dev
```

### Backend

Go to 'backend' directory

```bash
cd backend
```

#### Setup Virtual Environtmet

Install venv

```bash
pip install venv
```

Create venv

```bash
virtualenv venv
```

Activate venv

```bash
source venv/Scripts/activate  # Windows (bash)
source venv/bin/activate      # WSL / Linux / Mac
```

#### Install Dependencies

Install the required dependencies

```bash
pip install -r requirements.txt
```

#### Run

```bash
python app.py
```

#### Deactivate Virtual Environment

```bash
deactivate
```


## Authors

Created by

- [@satriadhikara](https://github.com/satriadhikara)
- [@caernations](https://github.com/caernations)
- [@fnathas](https://github.com/fnathas)