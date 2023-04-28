CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "Dependencies" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Dependencies" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Version" TEXT NOT NULL,
    "ContentsB64" BLOB NOT NULL
);

CREATE TABLE "Users" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230423183147_InitialiseDB', '8.0.0-preview.3.23174.2');

COMMIT;

