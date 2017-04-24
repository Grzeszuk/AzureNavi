//Adres aplikacji: http://itadscanner.azurewebsites.net/
	
    //Ścieżki:
	[Route("/info/{username}/{id}/{password}/{hashcode}", "GET")] // użytkownik dostaje info o firmie po id
    [Route("/points/{username}/{qrID}/{password}/{hashcode}}","GET")] // użytkownik dostaje punkty za kod
	[Route("/ask/{ID}/{password}", "GET")] // użytkownik dostaje pytania po id kodu np. qMEM to dostaje pytania o id qMEM1 oraz qMEM2 (1 i 2 dodawane automatycznie)
    [Route("/check/{username}/{ID1}/{answer1}/{ID2}/{answer2}/{password}/{hashcode}}", "GET")] // sprawdzanie odpowiedzi na pytania
	[Route("/register/{username}/{password}", "GET")] // rejestracja użytkownika , jeśli nie istnieje w bazie , jeśli istnieje aplikacja nic nie robi
    [Route("/userinfo/{username}/{password}/{hashcode}", "GET")] // dostajemy ilość punktów użytkownika i ilość zeskanowanych kodów

	// Password nie jest użytkownika 
	
	// Kodów nie da się wykorzystać 2 razy
	
	// Baza kodów to 3 pliki xml: Points,Informations,Questions
	
	// ID kodu to string
	
	// informacje zapisane w kodzie:
	    //pierwsza litera: p to punkty
		//pierwsza litera: q to pytania
		//pierwsza litera: i to informacje
		//reszta liter: to id kodu