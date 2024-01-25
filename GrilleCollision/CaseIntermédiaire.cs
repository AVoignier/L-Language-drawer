using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuadTree_OpenTK.Affichage.Modeles;
using QuadTree_OpenTK.GrilleCollision.Items;

namespace GrilleCollision.GrilleCollision
{
    internal class CaseIntermédiaire : Case
    {
        // 4 -> Cette Case
        // 0 1
        // 2 3

        Case[] casesFilles = new Case[4];

        public CaseIntermédiaire(Vec2 position, float largeur, float hauteur, Case? parent) : base(position, largeur, hauteur, parent)
        {
            items = new List<IItem>();
        }

        public override Case AjouterItem(IItem item)
        {
            byte idCase = getIdCaseItem(item);

            if(idCase == 4)
            {
                items.Add(item);
                return this;
            }
            else
            {
                if (casesFilles[idCase] == null)
                {
                    casesFilles[idCase] = new CaseIntermédiaire(this.PositionCaseFille(idCase), largeur/2, hauteur/2, this);
                }

                return casesFilles[idCase].AjouterItem(item);
            }
            
        }

        public List<Case> AjouterItems(List<Point> items)
        {
            List<Case> CaseItems = new List<Case>();

            foreach (Point item in items)
            {
                CaseItems.Add( AjouterItem(item));
            }
            return CaseItems;
        }

        protected byte getIdCaseItem(IItem item)
        {
            Vec2 centreCase = new Vec2( this.position.X() + this.largeur/2, this.position.Y() + this.hauteur/2 );

            if(this.profondeur == PROFONDEUR_MAX)
            {
                return 4;
            }
            else if( item.PointPlusADroite().X() < centreCase.X())
            {
                if( item.PointPlusHaut().Y() < centreCase.Y())
                {
                    return 2; 
                }
                else if( item.PointPlusBas().Y() > centreCase.Y() ) 
                {
                    return 0;
                }
            }
            else if(item.PointPlusAGauche().X() > centreCase.X())
            {
                if (item.PointPlusHaut().Y() < centreCase.Y())
                {
                    return 3;
                }
                else if (item.PointPlusBas().Y() > centreCase.Y())
                {
                    return 1;
                }
            }

            return 4;


        }

        private Vec2 PositionCaseFille(byte idCaseFille)
        {
            if( idCaseFille == 0)
            {
                return new Vec2(position.X(), position.Y() + hauteur/2) ;
            }
            else if( idCaseFille == 1)
            {
                return new Vec2( position.X() + largeur/2, position.Y() + hauteur/2 );
            }
            else if(idCaseFille == 2)
            {
                return new Vec2( position.X(), position.Y());
            }
            else
            {
                return new Vec2( position.X() + largeur /2, position.Y());
            }

        }

        public override string ToString()
        {
            string texte = "";

            for(int i=0; i < profondeur; i++)
            {
                texte += " ";
            }

            texte += "Case Intermédiaire " + base.ToString() + " \r\n ";
            
            for(int i=0; i < 4; i++)
            {
                if( casesFilles[i] != null )
                    texte += casesFilles[i].ToString();
            }

            return texte;
        }

        public override List<Vec2[]> getAllFormes()
        {
            List <Vec2[]> pointsFormes = new List<Vec2[]>();
            List<Vec2[]> pointsFormesTemp = new List<Vec2[]>();

            pointsFormes.Add(this.getForme());

            foreach( Case caseFille in this.casesFilles)
            {
                if(caseFille != null)
                {
                    pointsFormesTemp = caseFille.getAllFormes();
                    
                    foreach( Vec2[] lv in pointsFormesTemp)
                    {
                        pointsFormes.Add(lv);
                    }
                }
            }

            return pointsFormes;
        }

        public override List<Modele?> getAllModeles()
        {
            List<Modele?> modeles = new List<Modele?>();
            List<Modele?> modelesTemp = new List<Modele?>();

            foreach (IItem item in items)
            {
                modeles.Add(item.modele);
            }

            foreach( Case casefille in casesFilles)
            {
                if(casefille != null)
                {
                    modelesTemp = casefille.getAllModeles();
                    foreach(Modele modele in modelesTemp)
                    {
                        if(modele != null)
                        {
                            modeles.Add(modele);
                        }
                    }

                }
            }

            return modeles;

        }
    }
}
