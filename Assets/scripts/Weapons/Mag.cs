
public class Mag 
{
    //Script que representa un clip de munici�n
    public int currentAmmo, maxAmmo;
    public string ammoType;

    public Mag(int myMaxAmmo, string myType)
    {
        maxAmmo = myMaxAmmo;
        currentAmmo = maxAmmo;
        ammoType = myType;
    }
}
