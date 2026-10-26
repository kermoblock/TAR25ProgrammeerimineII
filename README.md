
1. Milline on DBContexti ülessane
   
Vastus: DBContexti ülessane on ühendada kood koos andmebaasiga
   
   ---
   
2. Miks ei ole hea paigutada kõik andmebaasipäringuid otse kasutajaliidese koodiga

Vastus:
Kõiki andmebaasipäringuid ei ole hea paigutada otse kasutajaliidese koodi, sest see muudab rakenduse vähem turvaliseks, raskemini hooldatavaks ja keerulisemini laiendatavaks. Seetõttu kasutatakse tavaliselt kolmeosalist arhitektuuri: kasutajaliides, serveripoolne äriloogika ja andmebaas.
  ---
3. Millisese kihti paigutaksid andmebaasiga suhtlemise MVC- või VIPER-arhitektuuris? Põhjenda oma vastust
   
Vastus:
MVC: Hoiab andmeid ja teostab CRUD-operatsioone. Kuhu paigutaksin? Modeli kihti Põhjendus: Eraldab andmebaasi loogika esitlusest ja Controllerist. Kontroller ei pea teadma SQL-päringutest ega andmebaasi mootorist midagi, vaid küsib Mudelilt valmis andmed.

VIPER: Hangib andmed andmebaasist ja annab need puhtal kujul Interactorile. Kuhu paigutaksin? DbContext Interactori taha eraldi teenusekihti (andmekihti). Põhjendus: Järgib ranget vastutuse jagamist. Interactor tegeleb ainult äriloogikaga (mida andmetega teha) ja teenusekiht tegeleb tehnilise andmepäringuga (kust andmeid võtta).
