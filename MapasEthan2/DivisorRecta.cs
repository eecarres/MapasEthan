using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.Internals;
using GMap.NET.WindowsForms.Markers;
using System.Drawing;

namespace MapasEthan2
{
    class DivisorRecta
    {

        static public List<Recta> listaRectas = new List<Recta>();

        static public List<PointLatLng>listaPuntosPoligono1 = new List<PointLatLng>();
        static public List<PointLatLng> listaPuntosPoligono2 = new List<PointLatLng>();

        

        static public List<List<PointLatLng>> Separacion(List<PointLatLng> puntosPoligono, Recta rectaDivisoria, List<GMapPolygon> listaPoligono)
        {
        // Entra un polígono (con sus puntos), una recta, que dividirá en dos al polígono, y  la lista de polígonos, para poder escribir los resultados de cada división en particular
        
            // Asignamos el poligono inicial al array de poligonos:

           




        //Encontramos las rectas que forman el polígono, y comprobamos cada una de las intersecciones.
            
            int utmzone=Utilidades.ZonaUtm(puntosPoligono[0]);

            PointF puntoInterseccion1=PointF.Empty;
            PointF puntoInterseccion2=PointF.Empty;

            SacaRectas(puntosPoligono);

            int rectaControl1 = 0, rectaControl2 = 0;
            
            for(int i=0; i<listaRectas.Count;i++)
            {
                PointF puntoInterseccion = InterseccionLinea(rectaDivisoria, listaRectas[i]);

                if (puntoInterseccion!=PointF.Empty) // Siempre que exista el punto
                {
                    // Y el punto esté entre los valores de X de la línea del polígono ( comprueba que el punto sea parte del polígono
                    if ((listaRectas[i].inicioF.X<puntoInterseccion.X && puntoInterseccion.X<listaRectas[i].finalF.X) || (listaRectas[i].inicioF.X>puntoInterseccion.X && puntoInterseccion.X>listaRectas[i].finalF.X) )
	                            {
                                    // Y el punto esté entre las Y de la linea del polígono, por el mismo motivo
		                            if ((listaRectas[i].inicioF.Y<puntoInterseccion.Y && puntoInterseccion.Y<listaRectas[i].finalF.Y) || (listaRectas[i].inicioF.Y>puntoInterseccion.Y && puntoInterseccion.Y>listaRectas[i].finalF.Y))
                                        	{
		                                            // Solo con todas esas condiciones el punto será el que buscamos. Lo guardamos como uno de los puntos de intersección
                                                   
                                                    
                                                    if (puntoInterseccion1==PointF.Empty)
	                                                   {
		                                                    puntoInterseccion1=puntoInterseccion;
                                                        rectaControl1=i;
	                                                   }
                                                    else if(puntoInterseccion2==PointF.Empty)
                                                    {
                                                    puntoInterseccion2=puntoInterseccion;
                                                        rectaControl2=i;
                                                    }
                                                    else
                                                    {MessageBox.Show("Liada parda, mas de dos puntos de interseccion!!");}
   
                                        	}
	                             }
                }

            }
            if (puntoInterseccion1 == PointF.Empty)
                return null;    
            // Definimos dos polígonos con los puntos encontrados:
                
            // Definimos P1 y P2 como PointLatLng de los puntos de interseccion encontrados:

            PointLatLng P1=new PointLatLng();
            P1 = PointLatLngAlt.FromUTM(utmzone,System.Convert.ToDouble(puntoInterseccion1.X), System.Convert.ToDouble(puntoInterseccion1.Y ));

            PointLatLng P2=new PointLatLng();
            P2 = PointLatLngAlt.FromUTM(utmzone,System.Convert.ToDouble(puntoInterseccion2.X), System.Convert.ToDouble(puntoInterseccion2.Y ));

            // Primer polígono: Empieza en P2, luego P1 y recorre poligono hasta P2 de nuevo:
            listaPuntosPoligono1.Add(P2);
            listaPuntosPoligono1.Add(P1);
            
                for (int i = rectaControl1; i < rectaControl2; i++)
                {
                    listaPuntosPoligono1.Add(puntosPoligono[i+1]);
                }

            listaPuntosPoligono1.Add(P2);

            // Segundo polígono: Empieza en P1, luego P2 y recorre poligono hasta P1 de nuevo:
            listaPuntosPoligono2.Add(P1);
            listaPuntosPoligono2.Add(P2);
            int x = rectaControl2;
            int iteraciones = puntosPoligono.Count()-(rectaControl2 - rectaControl1);
            for (int i = 0; i < iteraciones; i++)
            {
                x++;
                if (x==(puntosPoligono.Count()))
                {
                    x = 0;
                } 
                listaPuntosPoligono2.Add(puntosPoligono[x]);
            }
             
            listaPuntosPoligono2.Add(P1);

            // Con los dos polígonos definidos, devolvemos los puntos de los mismos al Formulario principal:

            List<List<PointLatLng>> resultado=new List<List<PointLatLng>>();
            resultado.Add(listaPuntosPoligono1);
            resultado.Add(listaPuntosPoligono2);

            return resultado;
        }


        public static PointF InterseccionLinea(Recta rectaDivisoria,Recta rectaPoligono)
        {
            PointF start1=rectaDivisoria.inicioF;
            PointF end1=rectaDivisoria.finalF;
            PointF start2=rectaPoligono.inicioF;
            PointF end2 = rectaPoligono.finalF;

            float denom = ((end1.X - start1.X) * (end2.Y - start2.Y)) - ((end1.Y - start1.Y) * (end2.X - start2.X));

            //  AB & CD are parallel 
            if (denom == 0)
                return PointF.Empty;

            float numer = ((start1.Y - start2.Y) * (end2.X - start2.X)) - ((start1.X - start2.X) * (end2.Y - start2.Y));

            float r = numer / denom;

            float numer2 = ((start1.Y - start2.Y) * (end1.X - start1.X)) - ((start1.X - start2.X) * (end1.Y - start1.Y));

            float s = numer2 / denom;

            if ((r < 0 || r > 1) || (s < 0 || s > 1))
                return PointF.Empty;

            // Find intersection point
            PointF result = new PointF();
            result.X = start1.X + (r * (end1.X - start1.X));
            result.Y = start1.Y + (r * (end1.Y - start1.Y));

            return result;
        }

        public static void SacaRectas(List<PointLatLng> puntosWGSPoligono)
        {
            listaRectas.Clear();
            puntosWGSPoligono.Add(puntosWGSPoligono[0]); // Full loop
            for (int i = 1; i < puntosWGSPoligono.Count; i++)
            {
                PointLatLng P1 = puntosWGSPoligono[i - 1];
                PointLatLng P2 = puntosWGSPoligono[i];
                Recta recta = new Recta(P1, P2);
                listaRectas.Add(recta);
            }
            puntosWGSPoligono.RemoveAt(puntosWGSPoligono.Count - 1);


        }
        public static void SacaRectas(List<double[]> puntosUTMPoligono, int utmzone)
        {
            listaRectas.Clear();
            puntosUTMPoligono.Add(puntosUTMPoligono[0]); // Full loop
            for (int i = 1; i < puntosUTMPoligono.Count; i++)
            {
                double[] P1 = puntosUTMPoligono[i - 1];
                double[] P2 = puntosUTMPoligono[i];
                Recta recta = new Recta(P1, P2, utmzone);
                listaRectas.Add(recta);
            }
            puntosUTMPoligono.RemoveAt(puntosUTMPoligono.Count - 1);
        }
    }
}
