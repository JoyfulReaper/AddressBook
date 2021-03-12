-- Script Date: 11-Mar-21 5:03 PM  - ErikEJ.SqlCeScripting version 3.5.2.80
-- Database information:
-- Database: C:\Users\rmf91\source\repos\AddressBook\AddressBook\AddressBookMVC\AddressBook.db
-- ServerVersion: 3.24.0
-- DatabaseSize: 48 KB
-- Created: 18-Jan-21 10:43 AM

-- User Table information:
-- Number of tables: 4
-- Addresses: -1 row(s)
-- EmailAddresses: -1 row(s)
-- People: -1 row(s)
-- PhoneNumbers: -1 row(s)

SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
INSERT INTO [People] ([Id],[FirstName],[LastName]) VALUES (
1,'Jordan','Bell');
INSERT INTO [People] ([Id],[FirstName],[LastName]) VALUES (
2,'Lorenzo','Laing');
INSERT INTO [People] ([Id],[FirstName],[LastName]) VALUES (
3,'Tyra','Miller');
INSERT INTO [People] ([Id],[FirstName],[LastName]) VALUES (
4,'Alex','Blevins');
INSERT INTO [People] ([Id],[FirstName],[LastName]) VALUES (
5,'Theresa','Dunn');
INSERT INTO [PhoneNumbers] ([Id],[PersonId],[Number],[IsPrimary]) VALUES (
1,1,355502515,0);
INSERT INTO [PhoneNumbers] ([Id],[PersonId],[Number],[IsPrimary]) VALUES (
2,2,770105692,1);
INSERT INTO [PhoneNumbers] ([Id],[PersonId],[Number],[IsPrimary]) VALUES (
3,3,3238103214,1);
INSERT INTO [PhoneNumbers] ([Id],[PersonId],[Number],[IsPrimary]) VALUES (
4,4,7147481567,1);
INSERT INTO [PhoneNumbers] ([Id],[PersonId],[Number],[IsPrimary]) VALUES (
5,5,2175550122,0);
INSERT INTO [EmailAddresses] ([Id],[PersonId],[EmailAddress],[IsPrimary]) VALUES (
1,1,'j_bell@protonmail.com',0);
INSERT INTO [EmailAddresses] ([Id],[PersonId],[EmailAddress],[IsPrimary]) VALUES (
2,2,'l.laing@yahoo.com.au',1);
INSERT INTO [EmailAddresses] ([Id],[PersonId],[EmailAddress],[IsPrimary]) VALUES (
3,3,'tyra_miller54@gmail.com',0);
INSERT INTO [EmailAddresses] ([Id],[PersonId],[EmailAddress],[IsPrimary]) VALUES (
4,4,'alex_blevins@live.com.au',1);
INSERT INTO [EmailAddresses] ([Id],[PersonId],[EmailAddress],[IsPrimary]) VALUES (
5,5,'theresa_dunn@gmail.com',0);
INSERT INTO [Addresses] ([Id],[PersonId],[StreetAddress],[City],[Suburb],[State],[PostCode],[IsMailAddress],[IsPrimary]) VALUES (
1,1,'72 Girvan Grove','Robinvale Irrigation District Section B','Sydenham','Victoria','3219',1,0);
INSERT INTO [Addresses] ([Id],[PersonId],[StreetAddress],[City],[Suburb],[State],[PostCode],[IsMailAddress],[IsPrimary]) VALUES (
2,2,'70 Rockhampton Street','Brisbane','Dutton Park','Queensland','4102',1,1);
INSERT INTO [Addresses] ([Id],[PersonId],[StreetAddress],[City],[Suburb],[State],[PostCode],[IsMailAddress],[IsPrimary]) VALUES (
3,3,'1336  Parkway Street','Brawley','San Bernadino','California','92227',0,1);
INSERT INTO [Addresses] ([Id],[PersonId],[StreetAddress],[City],[Suburb],[State],[PostCode],[IsMailAddress],[IsPrimary]) VALUES (
4,4,'90 Laurel Drive','Santa Monica','Sunset Park','California','90403',1,0);
INSERT INTO [Addresses] ([Id],[PersonId],[StreetAddress],[City],[Suburb],[State],[PostCode],[IsMailAddress],[IsPrimary]) VALUES (
5,5,'679 East Wood Drive
','Decatur','Oakhurst','Georgia','30030',1,1);
COMMIT;

