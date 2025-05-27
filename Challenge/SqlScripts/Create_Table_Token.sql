CREATE TABLE "Token"
(
    "Id" NVARCHAR(MAX) NOT NULL,
    "RefreshToken" NVARCHAR(MAX),
    "JwtId" NVARCHAR(MAX),
    "UserId" NVARCHAR(MAX),
    "User" NVARCHAR(MAX),
    "ExpiredAt" NVARCHAR(MAX),
    "CreatedAt" NVARCHAR(MAX),
    "UpdatedAt" NVARCHAR(MAX),
    "Id" NVARCHAR(MAX),
    CONSTRAINT "Token_pkey" PRIMARY KEY ("Id")
)

