-- Table: public.Ticket

-- DROP TABLE IF EXISTS public."Ticket";

CREATE TABLE IF NOT EXISTS public."Ticket"
(
    "IdTicket" serial NOT NULL DEFAULT nextval('"Ticket_IdTicket_seq"'::regclass),
    "Title" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Description" text COLLATE pg_catalog."default",
    "UserId" integer,
    "Status" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Ticket_pkey" PRIMARY KEY ("IdTicket")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."Ticket"
    OWNER to postgres;