CREATE TABLE IF NOT EXISTS public."User"
(
    "IdUser" serial NOT NULL DEFAULT nextval('"User_IdUser_seq"'::regclass),
    "UserName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "IsAdmin" boolean NOT NULL DEFAULT false,
    CONSTRAINT "User_pkey" PRIMARY KEY ("IdUser")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."User"
    OWNER to postgres;