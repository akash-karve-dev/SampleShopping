

CREATE TABLE IF NOT EXISTS public."Users" (
	"Id" uuid NOT NULL,
	"Name" varchar NOT NULL,
	"Email" varchar NOT NULL,
	"CreatedAt" TIMESTAMP NOT NULL,
	CONSTRAINT users_pk PRIMARY KEY ("Id"),
	CONSTRAINT name_uq UNIQUE ("Name"),
	CONSTRAINT email_uq UNIQUE ("Email")
);

INSERT INTO public."Users"( "Id", "Name", "Email", "CreatedAt")
VALUES
(
'292a485f-a56a-4938-8f1a-bbbbbbbbbbb1'::UUID,
'Akash',
'akash.karve@test.com',
 current_timestamp
),
(
'1476700f-c96c-49ac-b69f-82ab7989cc95'::UUID,
'Sushmita',
'sushmita.karve@test.com',
 current_timestamp
);