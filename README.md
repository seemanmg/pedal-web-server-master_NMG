# pedal-web-server

**Address** : The address of a gym
* AddressId
* StreetLine1
* StreetLine2
* City
* TypeStateId
* Zip

**ContactInfo** : The contact information of a user or gym, or gym owner
* ContactInfoId
* Email
* Phone
* AddressId

**FriendInvites** : Friend Invite record if another user signs up with their code
* FriendInviteId
* UserId
* FriendEmail
* InviteDate
* CreditsReward

**GiveCredits** : Any credits being passed to another user for any reason. UserIdWhoGiveCredits is Null if the admin is adding credits for their subscription
* GiveCreditsId
* AmountToGiveNewUser
* UserIdToGiveCredits
* DateTime
* UserIdWhoGiveCredits
* AmountToGiveReferer

**GymPasses** : The passes the Users bought to go to the Gyms
* Id
* UserId
* GymId
* LocalDateBought
* LocalDateExpired
* CreditsUsed
* AmountCharged
* ServerTimeDateBought
* GymPassCost


**GymPhotoGallery** : The photos in the gym
* GymPhotoGalleryId
* GymId
* Photo
* IsCoverPhoto

**Gyms** : The gyms everyone is working out at
* GymId
* GymName
* GymInfo
* ApplicationDate
* **IsActive** - Gym is still around and hasn't cancelled our contract
* **CreditDollarValue** - Amount to pay the gym
* GymGuid
* **IsApproved** - We are showing the gym in the app
* Position
* Url
* OwnerName
* **PriceToCharge** - Amount to charge the user for a pass
* CurrentToken
* **ScheduleUrl** - Optional url for their schedule page
* StripeUrl
* **PublicContactInfoId** - Gym's public contact information to show in the app
* **OwnerContactInfoId** - Owner's personal contact information for us to contact them
* **ActiveDate** - Date gym became listed in the network


**GymSchedule** : Hours a gym is open
* GymScheduleId
* GymId
* TypeWeekDayId
* StartTime
* EndTime
* Flag

**Logs** : History of any action or error
* LogId
* LogMessage
* InsertDate
* **IsError** - If its an Error log or an Action log

**PromoCodes** : Avaiable promo codes and what their rewards are
* Id
* Code
* PromoDescription
* CreditValue

**PromoCodesUsed** : Record of any promo codes used
* Id
* UserId
* PromoCodeId
* DateUsed

**TypeState** : The 50 states (Illionois, Utah, etc)
* TypeStateId
* StateName
* StateAbbreviation

**TypeWeekDay** : The days of the week (Monday, Tuesday, etc)
* TypeWeekDayId
* TypeDescription

**Users** : The Users going to workout 
* UserId
* ContactInfoId
* UserGuid
* **hasCreditCard** - Credit Card is in system
* FirstName
* LastName
* Email
* **hasActiveSubscription** - Subscription is autorenewing
* StripeUrl
* CurrentToken
* Credits
* joinDate
* **ReferalUrl** - Code to refer a friend to sign up with
