# Project - ISOOU - Information System For Primary Schools (Информационна система за Обслужване на Основните училища)

## Type - Management System

## Description

This is a Management system which helps parents to register their children at Primary School. 
The system shows information about school, calculates scores of candidates depending on their data and the addmission criteria.
Guest Users can see common information, access all the public schools' information, search free spots by district and year of birth of him/his child, Register and Login to their accounts.
Regular Users can fill data about own information alse his/him child, fill the form to apply, uplodad documents.
Director can see info about the school where he/she is working,
Administrators can create and perform admission procedure

## Entities

### ApplictionUser >> Candidates
  - Id (string)
  - Email (string) 
  - Password (string)
  - Full Name (string)
  - Phone Number (string)
  - EGN(string)
  - DateOfBirth(string)
  - PlaceOfBirth(string)
  - Sex(char)
  - MothersFullName(string)
  - FathersFullName(string)
  - MothersPhoneNumber(string)
  - FathersPhoneNumber(string)
  - MothersPlaceOfWork(string)
  - FathersPlaceOfWork(string)
  - Status (bool)false=not admitted, true=admitted
  - List of Schools to apply
  
 ### School
  - Id (string)
  - Name (string) 
  - Address (AddressDetails)
  - Director (ApplicationUser)
  - List of candidates, CandidatesId
 
  
### AdmissionProcedure
  - Id (string)
  - StartDate Apply Documents (DateTime)
  - EndDate Apply Documents (DateTime)
  - StartDate Enrollment (DateTime)
  - EndDate Enrollment (DateTime)
  - Schools (School)
	
### DocumentSubmission
  - Id (string)
  - School (School)
  - Candidate(ApplicationUser)
  - DateTimeUploaded (DateTime)
  - PathFile(string)
  
  ### AddressDetails	
	- Id
	- PermanentAddress
	- CurrentAddress
	- District
	- Quarter
	
	### AddmissionCriteria
	- Id
	- List<string> criteria
	- School 
