# PatientChallenge

## Index

[About](#about)<br>
[Pre-requisites](#pre-requisites)<br>
[Installation](#installation)<br>
[Running the project](#running-the-project)<br>
[Unit testing](#unit-testing)<br>
[Known issues](#known-issues)<br>
[Notes](#notes)

## About

This project is an application developer interview test, a CRUD done using .NET 6 and ReactJs, you must be able to authenticate and validate a Patient model.

The main goal is to prove my programming skills and show what I am capable of.

## Pre requisites

1. Visual studio 2022
2. SQL Server Express LocalDB
3. NodeJS 18.16.0 or higher
4. NPM

## Installation

1. Clone the repository.
2. Make sure your connection string is pointing to your LocalDB.
3. Navigate to PatientChallenge\PatientChallenge.Site\patientchallengesite\ folder where package.json is located.
4. Open a terminal command prompt and type npm install.

If you don't encounter any issues, then you are ready to start.

## Running the project 

1. Open the solution using Visual Studio 2022 and then, click Run.
2. Navigate to PatientChallenge\PatientChallenge.Site\patientchallengesite\ folder.
3. Open the terminal command prompt and type: npm start

## Unit Testing

Inside the Visual Studio solution you will find a test suite made for every endpoint using Moq.

## Known issues

- The models were made without DTOs [See information about DTOs](https://go.microsoft.com/fwlink/?linkid=2123754).
- The role is just a demonstration property and both the user and the administrator have the same rights.
- PatientChallenge.Site could be optimized with DRY programming principle practices.
## Notes

You will count with a master credential which is 
<br>
username: admin
<br>
password: 4dmin

Database is auto-created and auto-populated during the first run by the DatabaseInitializer class.