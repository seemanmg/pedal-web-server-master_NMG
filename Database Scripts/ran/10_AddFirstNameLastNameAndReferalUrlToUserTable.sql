ALTER TABLE Users ADD FirstName [varchar](250) NULL;
ALTER TABLE Users ADD LastName [varchar](250) NULL;
ALTER TABLE Users ADD ReferalUrl [varchar](50) NULL;
ALTER TABLE Users ADD Email [varchar](250) NULL;
ALTER TABLE Users ADD ReferalCredits int NULL;
ALTER TABLE Users ADD IsUnlimited bit NULL;
ALTER TABLE Users ADD StripeUrl [varchar](250) NULL;
ALTER TABLE Users ADD CardType [varchar](250) NULL;
ALTER TABLE Users ADD CurrentToken [varchar](250) NULL;

ALTER TABLE Users ADD AcceptedTerms bit NULL;


ALTER TABLE Gyms ADD Position geography NULL;
ALTER TABLE Gyms ADD IsMindBody bit  NULL;
ALTER TABLE Gyms ADD Url [varchar](250) NULL;


