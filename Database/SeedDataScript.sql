USE NotesDB
GO

--inserting users data
insert into Users (Id, PasswordHash, PasswordSalt, Username) Values ('523F6981-6479-4FBB-BCC8-2C21A94D3CA2', CONVERT(varbinary(max),0x261DCE4B1CE2D0B665218A3F3779C92DEC1E14F68A600D8C2756B1055E40931F42FB96E5AE66455347426DA47236B8941F95E1E301A72D835EC841EC96C1CA00),
CONVERT(varbinary(max), 0x69B2B63C91929400AABA729A7FDAF2675AA3879F1D960B2232066EF49895C0912C914EEC4166DC32D391876F016DCFA220B9198D1B463AECA11A9BC56C2F7D42399AE171344B4A98117D3FC045C393708D17584572DD6DB534F8261366BCB3CD9947141FE483BA9180CC46BF6FA0266F9273798E6A601859481A03FA7FFDBF18),'thor.ragnarok');

insert into Users (Id, PasswordHash, PasswordSalt, Username) Values ('EC24D437-2276-4F29-B469-2AB3A339AA19', CONVERT(varbinary(max),0xA614360B89F917E63A4F628EC29AA9C11D63F4DE3359A4B3FAC9AAB4072A3635854943D4E3DE0DC5085415EB8E1B8DCEBF8C338A6C118ACE2D2D61DEA92BBE8B),
CONVERT(varbinary(max), 0x4184BCEA626670FC1A9CB56E4C432A1C16AC4A168A18BCAF12BC2CCF92698B27D72DBD96B60615DC42EAE590A7639BBB55024B9EA001BF60B0CBF921DD0AD9EBE7AA630D984A5745774DE181EE4EA55A54210F38669707F20669445F493F5A2CD9C178F666D9BD26CB4869A0E81CAAC0B7F00D71B185C6770524948CF3846F03),'cocacola');

insert into Users (Id, PasswordHash, PasswordSalt, Username) Values ('1BE8D62E-F073-4555-84EF-2D4B9F2EC4E0', CONVERT(varbinary(max),0xCD75374BDC34F8B63A87F9C280EF936136547F91870A68FCE4B4EEF9119AA600DC05E92705A0159837502585E696A3E4D45825BF091F01389E1A01B1C6F11A64),
CONVERT(varbinary(max), 0xF7812B9CF99E2BAF87042811D1D3CE822F49DB2D62BC9F9BCD0B3B8D86DE8391AD8CA1D0C2BD53484F6E8FC04C2BA74E79509949FE0F2586188C84243C1D2F4B41518F1FBB59B4DD9ADC119AEF02BC4C766D8500A5F67F7E5EBB0ECAC9A58D77BAB4880DB50F46394BB2DB8E5FFDE52A82227DAE45FD672DD39215E0E6248A67),'out.of.ideas');

insert into Users (Id, PasswordHash, PasswordSalt, Username) Values ('1F08972B-AD12-468B-9473-77AECB0D5F52', CONVERT(varbinary(max),0x4B32C1CA91856D9DD0087F72AE740C8638521D9558927791F508160465B3CFB6E05F68A5EEDA154763CE9740614E1AA3A6E1BA1A77CB591A50354BFB2F0C68AB),
CONVERT(varbinary(max), 0xF756C96F0D9F84290ABE80F5D1CA9A047983BD49D5FF84B135C253CF62D2C9B49207ABA747CED19D9FD44BB295A5561E2A207BF1A1E7EA16DBA4CA69F3BEF3D4A0B382F9090197FA4EAEF274137A7A3EABBB06C252112D17CE1D235832CE7C06EACECBB0BA99091784B5398D74F79BB7941B01B041BF4D8282EB200A1E05C353),'blaz.rupnik');

insert into Users (Id, PasswordHash, PasswordSalt, Username) Values ('B87CE8BD-DEAB-4B1C-93FB-65C69C94FC18', CONVERT(varbinary(max),0x82F88D122683D82C7C8D99A5FD7663BE7B75FEFFB913469AC40A04CB58B2F5B43CE59211C8C60E88A5ED285DD2A2D011EFD083A73223EEE6554B4B9EA99B754C),
CONVERT(varbinary(max), 0xB6F61A5589E489844E48A61267D79EBECB506BF04D49B64F98EAFD0A3D59B66972F79868F38AE04843C4DFB65C89937C5BEF76730725FE77C7AC346E316672E71842440D2A1619AA0E78373544DC23F96C9DAF6551A90759D6C9CAA967C558B0264FED748AB8EE84A60D0DA86E045122808B00624EA1095E2F83D8530572F763),'jebron.lames');
GO

--inserting folders data
-- thor ragnarok folders
insert into Folders (Id, Name, UserId) Values ('E639C650-DB2A-4650-A66D-B5628DC6BB53', 'Prva datoteka', '523F6981-6479-4FBB-BCC8-2C21A94D3CA2');
insert into Folders (Id, Name, UserId) Values ('001CB92E-5FA5-4185-98F7-578EEEE6B68B', 'Druga datoteka', '523F6981-6479-4FBB-BCC8-2C21A94D3CA2');
insert into Folders (Id, Name, UserId) Values ('6490BFD8-08E0-4857-85C8-727936B257B7', 'Tretja datoteka', '523F6981-6479-4FBB-BCC8-2C21A94D3CA2');
insert into Folders (Id, Name, UserId) Values ('2D2C640F-F4C7-49C1-91E6-C9C285567F09', 'Cetrta datoteka', '523F6981-6479-4FBB-BCC8-2C21A94D3CA2');
insert into Folders (Id, Name, UserId) Values ('023E46DD-AB3E-49C4-8729-7A022F9D94E4', 'Peta datoteka', '523F6981-6479-4FBB-BCC8-2C21A94D3CA2');

-- cocacola folders
insert into Folders (Id, Name, UserId) Values ('6D6AFAAC-6727-4CAC-8558-50E8145D7DF3', 'Programiranje 1', 'EC24D437-2276-4F29-B469-2AB3A339AA19');
insert into Folders (Id, Name, UserId) Values ('557B1F2F-B609-46FE-BF88-4DE885DDC912', 'Programiranje 2', 'EC24D437-2276-4F29-B469-2AB3A339AA19');
insert into Folders (Id, Name, UserId) Values ('C5D4B710-8260-47FA-ABF5-247D4F98CCEF', 'Algoritmi in podatkovne strukture', 'EC24D437-2276-4F29-B469-2AB3A339AA19');
insert into Folders (Id, Name, UserId) Values ('58397A26-E864-457F-9E80-DFA095A0C145', 'Racunalniske komunikacije', 'EC24D437-2276-4F29-B469-2AB3A339AA19');
insert into Folders (Id, Name, UserId) Values ('F941F4AD-1EED-4608-8B80-F1D680A754F9', 'Diskretne strukture', 'EC24D437-2276-4F29-B469-2AB3A339AA19');

-- out.of.ideas folders
insert into Folders (Id, Name, UserId) Values ('AF0B5857-481A-4448-933B-AE87ED0DAAD9', 'Programiranje 1', '1BE8D62E-F073-4555-84EF-2D4B9F2EC4E0');
insert into Folders (Id, Name, UserId) Values ('A270DFF4-AEBB-491C-A207-FD9041937F00', 'Osnove matematicne analize', '1BE8D62E-F073-4555-84EF-2D4B9F2EC4E0');
insert into Folders (Id, Name, UserId) Values ('FB4BB303-A9BD-4654-9001-504DB3CD07A6', 'Podatkovne baze', '1BE8D62E-F073-4555-84EF-2D4B9F2EC4E0');
insert into Folders (Id, Name, UserId) Values ('6074A562-33C9-49DD-8C51-F48AD75E2FF4', 'Principi jezikov', '1BE8D62E-F073-4555-84EF-2D4B9F2EC4E0');
insert into Folders (Id, Name, UserId) Values ('D85F6BF3-A0D8-400C-BBC0-6388CA6BC335', 'Diskretne strukture', '1BE8D62E-F073-4555-84EF-2D4B9F2EC4E0');

-- blaz.rupnik folders
insert into Folders (Id, Name, UserId) Values ('FF12B7D7-6A1D-49F0-98B1-5902AEFD91FD', 'Najljubsi nogometasi', '1F08972B-AD12-468B-9473-77AECB0D5F52');
insert into Folders (Id, Name, UserId) Values ('B83EA7AD-34BD-4793-B184-A74DE8ED690A', 'Testni program', '1F08972B-AD12-468B-9473-77AECB0D5F52');
insert into Folders (Id, Name, UserId) Values ('465208F3-88A9-4D3D-BA1E-E6457BF22C9C', 'Novoletne slike', '1F08972B-AD12-468B-9473-77AECB0D5F52');
insert into Folders (Id, Name, UserId) Values ('5BA7BFE1-664F-4B1E-9D9F-4E9AA54C9DD3', 'Zbirka podatkov', '1F08972B-AD12-468B-9473-77AECB0D5F52');
insert into Folders (Id, Name, UserId) Values ('4E734DED-6F69-4C04-A141-4D4A4CA2DBA6', 'Zapiski', '1F08972B-AD12-468B-9473-77AECB0D5F52');

-- jebron.lames folders
insert into Folders (Id, Name, UserId) Values ('0202D743-CF3C-4B91-ACE4-D543FCBA44EF', 'Seznam prijav', 'B87CE8BD-DEAB-4B1C-93FB-65C69C94FC18');
insert into Folders (Id, Name, UserId) Values ('0494758F-6F98-4CAE-9916-0223E4685D7E', 'Zapiski', 'B87CE8BD-DEAB-4B1C-93FB-65C69C94FC18');
insert into Folders (Id, Name, UserId) Values ('2C1C431C-E547-43EB-9C2F-26C0C2F8418E', 'Letni obracuni', 'B87CE8BD-DEAB-4B1C-93FB-65C69C94FC18');
insert into Folders (Id, Name, UserId) Values ('00B110DF-9DBB-445C-9501-9B1B9E2A5951', 'Pogodbe', 'B87CE8BD-DEAB-4B1C-93FB-65C69C94FC18');
insert into Folders (Id, Name, UserId) Values ('AD756A00-0431-49B5-92F1-E4B17F6382C4', 'Placilne liste', 'B87CE8BD-DEAB-4B1C-93FB-65C69C94FC18');
GO

--inserting notes data
insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'Random gibberish text','Lose eyes get fat shew. Winter can indeed letter oppose way change tended now. So is improve my charmed picture exposed adapted demands. Received had end produced prepared diverted strictly off man branched. Known ye money so large decay voice there to. Preserved be mr cordially incommode as an. He doors quick child an point at. Had share vexed front least style off why him. ','E639C650-DB2A-4650-A66D-B5628DC6BB53',1,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'Select HTML output from','Oh acceptance apartments up sympathize astonished delightful. Waiting him new lasting towards. Continuing melancholy especially so to. Me unpleasing impossible in attachment announcing so astonished. What ask leaf may nor upon door. Tended remain my do stairs. Oh smiling amiable am so visited cordial in offices hearted. ','E639C650-DB2A-4650-A66D-B5628DC6BB53',1,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'Then so does over sent dull on','Denote simple fat denied add worthy little use. As some he so high down am week. Conduct esteems by cottage to pasture we winding. On assistance he cultivated considered frequently. Person how having tended direct own day man. Saw sufficient indulgence one own you inquietude sympathize. ','E639C650-DB2A-4650-A66D-B5628DC6BB53',1,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'Domestic suitable bachel','He oppose at thrown desire of no. Announcing impression unaffected day his are unreserved indulgence. Him hard find read are you sang. Parlors visited noisier how explain pleased his see suppose. Do ashamed assured on related offence at equally totally. Use mile her whom they its. Kept hold an want as he bred of. Was dashwood landlord cheerful husbands two. Estate why theirs indeed him polite old settle though she. In as at regard easily narrow roused adieus.','E639C650-DB2A-4650-A66D-B5628DC6BB53',1,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'me answer do relied','
Suppose end get boy warrant general natural. Delightful met sufficient projection ask. Decisively everything principles if preference do impression of. Preserved oh so difficult repulsive on in household. In what do miss time be. Valley as be appear cannot so by. Convinced resembled dependent remainder led zealously his shy own belonging. Always length letter adieus add number moment she. Promise few compass six several old offices removal parties fat. Concluded rapturous it intention perfectly daughters is as. ','E639C650-DB2A-4650-A66D-B5628DC6BB53',1,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'Denote simple fat denied add worthy little use','Feet evil to hold long he open knew an no. Apartments occasional boisterous as solicitude to introduced. Or fifteen covered we enjoyed demesne is in prepare. In stimulated my everything it literature. Greatly explain attempt perhaps in feeling he. House men taste bed not drawn joy. Through enquire however do equally herself at. Greatly way old may you present improve. Wishing the feeling village him musical. ','E639C650-DB2A-4650-A66D-B5628DC6BB53',1,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'Oh smiling amiable am so visited cordial','
His followed carriage proposal entrance directly had elegance. Greater for cottage gay parties natural. Remaining he furniture on he discourse suspected perpetual. Power dried her taken place day ought the. Four and our ham west miss. Education shameless who middleton agreement how. We in found world chief is at means weeks smile',null,1,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'peculiar get joy doubtful','Suppose end get boy warrant general natural. Delightful met sufficient projection ask. Decisively everything principles if preference do impression of. Preserved oh so difficult repulsive on in household. In what do miss time be. Valley as be appear cannot so by. Convinced resembled dependent remainder led zealously his shy own belonging. Always length letter adieus add number moment she. Promise few compass six several old offices removal parties fat. Concluded rapturous it intention perfectly daughters is as. ',null,1,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'now devonshire diminution law','Debating me breeding be answered an he. Spoil event was words her off cause any. Tears woman which no is world miles woody. Wished be do mutual except in effect answer. Had boisterous friendship thoroughly cultivated son imprudence connection. Windows because concern sex its. Law allow saved views hills day ten. Examine waiting his evening day passage proceed',null,0,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'Small for ask shade water manor think men begin','
Certain but she but shyness why cottage. Gay the put instrument sir entreaties affronting. Pretended exquisite see cordially the you. Weeks quiet do vexed or whose. Motionless if no to affronting imprudence no precaution. My indulged as disposal strongly attended. Parlors men express had private village man. Discovery moonlight recommend all one not. Indulged to answered prospect it bachelor is he bringing shutters. Pronounce forfeited mr direction oh he dashwoods ye unwilling',null,0,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'vicinity contempt together in possible branched','Domestic confined any but son bachelor advanced remember. How proceed offered her offence shy forming. Returned peculiar pleasant but appetite differed she. Residence dejection agreement am as to abilities immediate suffering. Ye am depending propriety sweetness distrusts belonging collected. Smiling mention he in thought equally musical. Wisdom new and valley answer. Contented it so is discourse recommend. Man its upon him call mile. An pasture he himself believe ferrars besides cottag',null,0,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'was these three and songs arose whose','
Do greatest at in learning steepest. Breakfast extremity suffering one who all otherwise suspected. He at no nothing forbade up moments. Wholly uneasy at missed be of pretty whence. John way sir high than law who week. Surrounded prosperous introduced it if is up dispatched. Improved so strictly produced answered elegance is',null,0,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'the put instrument sir entreaties affronting','He an thing rapid these after going drawn or. Timed she his law the spoil round defer. In surprise concerns informed betrayed he learning is ye. Ignorant formerly so ye blessing. He as spoke avoid given downs money on we. Of properly carriage shutters ye as wandered up repeated moreover. Inquietude attachment if ye an solicitude to. Remaining so continued concealed as knowledge happiness. Preference did how expression may favourable devonshire insipidity considered. An length design regret an hardly barton mr figure.',null,0,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')

insert into Notes (Id, Name, Content, FolderId, IsShared, NoteTypeId, UserId) Values (newid(),
'him father parish looked has sooner','Of friendship on inhabiting diminution discovered as. Did friendly eat breeding building few nor. Object he barton no effect played valley afford. Period so to oppose we little seeing or branch. Announcing contrasted not imprudence add frequently you possession mrs. Period saw his houses square and misery. Hour had held lain give yet',null,0,'27A6378A-D5F1-437A-9324-FB42E7A0E1EF','523F6981-6479-4FBB-BCC8-2C21A94D3CA2')