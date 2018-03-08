ALTER TABLE Gyms ADD SignedContract bit NOT NULL DEFAULT 0;
ALTER TABLE PromoCodesUsed ADD WasClaimed bit NOT NULL DEFAULT 0;
ALTER TABLE PromoCodesUsed ADD ClaimedOn Datetime NULL;