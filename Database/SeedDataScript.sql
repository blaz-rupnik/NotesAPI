USE NotesDB
GO

insert into Users (Id, PasswordHash, PasswordSalt, Username) Values ('523F6981-6479-4FBB-BCC8-2C21A94D3CA2', CONVERT(varbinary(max),0x261DCE4B1CE2D0B665218A3F3779C92DEC1E14F68A600D8C2756B1055E40931F42FB96E5AE66455347426DA47236B8941F95E1E301A72D835EC841EC96C1CA00),
CONVERT(varbinary(max), 0x69B2B63C91929400AABA729A7FDAF2675AA3879F1D960B2232066EF49895C0912C914EEC4166DC32D391876F016DCFA220B9198D1B463AECA11A9BC56C2F7D42399AE171344B4A98117D3FC045C393708D17584572DD6DB534F8261366BCB3CD9947141FE483BA9180CC46BF6FA0266F9273798E6A601859481A03FA7FFDBF18),'thor.ragnarok');