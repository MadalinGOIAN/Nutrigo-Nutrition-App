## Nutrigo - Aplicație mobilă de nutriție
Proiectul de licență alcătuit din trei componente:
- bază de date,
- web API,
- aplicație mobilă.

În repository se vor găsi codurile sursă ale componentelor REST API și aplicației mobile, precum și cod SQL aferent bazei de date în vederea modificărilor unor anumite proprietăți ale tabelelor.

În directoarele dedicate API-ului web și al aplicației mobile se pot găsi fișierele de tip .sln pentru încărcarea proiectelor în mediul de dezvoltare Visual Studio în vederea compilării individuale ale acestora. Compilarea aplicației mobilă este indicat să se efectueze de pe un dispozitiv fizic și nu un emulator, întrucât va fi necesar accesul la camera de fotografiat al acestuia. Prin această cale, aplicația va și rămâne instalată în dispozitiv.

În vederea funcționării corecte a aplicației mobile, atât server-ul de tip „Azure Database for MySQL flexible server”, numit „bdlicenta”, cât și REST API-ul de tip „Web App”, numit „WebApiLicenta”, create de pe contul instituțional pe platforma cloud Microsoft Azure, vor trebui să fie pornite.

Fișierul „com.companyname.mobileapp.apk” reprezintă un executabil realizat pentru distribuția Android 13, ce poate fi descărcat și rulat local pe un dispozitiv mobil. Dacă rularea executabilului nu reușește, se poate opta pentru compilarea proiectului din Visual Studio, folosind telefonul mobil, după cum s-a descris anterior.

