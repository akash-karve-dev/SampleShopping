

CREATE TABLE public."Users" (
	"Id" uuid NOT NULL,
	"Name" varchar NOT NULL,
	"Email" varchar NOT NULL,
	CONSTRAINT users_pk PRIMARY KEY ("Id")
);