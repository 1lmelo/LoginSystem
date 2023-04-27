CREATE TABLE "Users" (
	"Id" INT NOT NULL IDENTITY(1,1),
	"Name" VARCHAR(100),
	"Email" VARCHAR(100),
	"Password" VARCHAR(50),
	PRIMARY KEY ("Id")
);


select * from Users