using GrilleCollision;
using GrilleCollision.GrilleCollision;
using QuadTree_OpenTK.Affichage.Modeles;
using QuadTree_OpenTK.GrilleCollision.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree_OpenTK.GrilleCollision;


internal class GrilleCollision
{
    
    Dictionary<IItem, Case> DictionnaireItemCase;
    Case CaseDepart;
    private static int largeur;
    private static int hauteur;

    private static GrilleCollision instance;

    public static void SetupGrilleCollision( int largeur, int hauteur)
    {
        GrilleCollision.largeur = largeur;
        GrilleCollision.hauteur = hauteur;
    }

    public static GrilleCollision getInstance()
    {
        if( instance == null)
        {
            instance = new GrilleCollision();
        }

        return instance;
    }

    private GrilleCollision()
    {
        CaseDepart = new CaseIntermédiaire(new Vec2(0, 0), hauteur, largeur, null);
        DictionnaireItemCase = new Dictionary<IItem, Case>();
    }

    public bool AjouterItem( IItem item)
    {
        if(item == null )
            return false;


        Case CaseItem = CaseDepart.AjouterItem(item);
        
        if(CaseItem != null)
        {
            DictionnaireItemCase.Add(item, CaseItem);
            return true;
        }
        return false;
    }

    public List<bool> AjouterItems(List<IItem> items)
    {
        List<bool> validationItem= new List<bool>();

        foreach(IItem item in items)
        {
            validationItem.Add( AjouterItem(item));
        }

        return validationItem;
    }

    public List<Vec2[] > getAllFormes()
    {
        return CaseDepart.getAllFormes();
    }

    public List<Modele?> getAllModele()
    {
        List<Modele?> modeles= new List<Modele?>();

        return CaseDepart.getAllModeles();
    }

    public override string ToString()
    {
        return CaseDepart.ToString();
    }

}
