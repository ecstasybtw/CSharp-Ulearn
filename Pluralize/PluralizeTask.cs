namespace Pluralize;

public static class PluralizeTask
{
	public static string PluralizeRubles(int count)
	{
		var lastTwoDigits = number % 100;
    	if (lastTwoDigits >= 11 && lastTwoDigits <= 19)
        	return "рублей";
    	var lastDigit = number % 10;
    	if (lastDigit == 1)
        	return "рубль";
    	else if (lastDigit >= 2 && lastDigit <= 4)
        	return "рубля";
    	else
        	return "рублей";
	}
}