using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;
using GMap.NET;
using System.Windows.Forms;

namespace MapasEthan2
{
    class Utilidades
    {
        // Metodo que devuelve la zona UTM del punto (en lat y long)
        public static int ZonaUtm(PointLatLng puntoInicial)
        {
            int utmzone = (int)((puntoInicial.Lng - -186.0) / 6.0); // Sacamos la zona utm del primer punto del polígono
            return utmzone;
        }

        // Pasa a cartesianas (array de double) el punto en Lat y Lng
        public static double[] PasarACartesianas(PointLatLng puntoEsfericas,int utmzone)
        {
            PointLatLngAlt pnt = new PointLatLngAlt(puntoEsfericas);
            double[] puntoSalida;

            puntoSalida = pnt.ToUTM();

            return puntoSalida;
           
        }

        // Pasa a Lat y Lng el punto en cartesianas (array de double)
        public static PointLatLng PasarAWGS(double[] puntoWGS, int utmzone)
        {
            //CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory(); // Creamos una instancia del convertidor de coordenadas

            //// Generamos los sistemas de coordenadas ( WGS84 es esfericas, UTM es cartesianas)
            //ICoordinateSystem WGS = GeographicCoordinateSystem.WGS84;
            //ICoordinateSystem UTM = ProjectedCoordinateSystem.WGS84_UTM(utmzone, true); // CAMBIAR SI VAMOS A HEMISFERIO SUD

            //ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(UTM, WGS); // Instancia del transformador de coordenadas


            //double[] puntoIntermedio = trans.MathTransform.Transform(puntoWGS);
            //PointLatLng puntoSalida = new PointLatLng();
            //puntoSalida.Lat = puntoIntermedio[0];
            //puntoSalida.Lng = puntoIntermedio[1];
            //return puntoSalida;

           
            PointLatLng puntoSalida;
            puntoSalida = PointLatLngAlt.FromUTM(utmzone, puntoWGS[0], puntoWGS[1]);
            return puntoSalida;
        }

        // Función de calculo de área del polígono, definida en el GRIDUI.cs, sin cambiar de nombre
        static public double calcpolygonarea(List<PointLatLng> polygon)
        {
            // Para el cálculo del area el polígono debe ser cerrado
            // Coordenadas del mapa están en lat long, hay que pasarlas a UTM para poder calcular áreas

            if (polygon.Count == 0) // Debe haber un polígono válido en la llamada al método
            {
                MessageBox.Show("Please define a polygon!");
                return 0;
            }

            // Cerramos el polígono (Último punto igual al primero), se deshace al finalizar el método
            if (polygon[0] != polygon[polygon.Count - 1])
                polygon.Add(polygon[0]); // make a full loop

            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory(); // Creamos una instancia del convertidor de coordenadas

            GeographicCoordinateSystem wgs84 = GeographicCoordinateSystem.WGS84; // Creamos el sistema de coordenadas en el que estan los puntos

            int utmzone = (int)((polygon[0].Lng - -186.0) / 6.0); // Sacamos la zona utm del primer punto del polígono

            IProjectedCoordinateSystem utm = ProjectedCoordinateSystem.WGS84_UTM(utmzone, polygon[0].Lat < 0 ? false : true); // Creamos el sistema de coordenadas en el que queremos obtener los puntos

            ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(wgs84, utm); // Instancia del transformador de coordenadas

            //Definimos  los productos escalares de cada lado del polígono
            double prod1 = 0;
            double prod2 = 0;

            for (int a = 0; a < (polygon.Count - 1); a++) // Recorremos el polígono, primero asignando puntos a arrays double, luego pasando a sistema cartesiano y por último calculando áreas con el método de los productos
            {
                double[] pll1 = { polygon[a].Lng, polygon[a].Lat };
                double[] pll2 = { polygon[a + 1].Lng, polygon[a + 1].Lat };

                double[] p1 = trans.MathTransform.Transform(pll1);
                double[] p2 = trans.MathTransform.Transform(pll2);

                prod1 += p1[0] * p2[1];
                prod2 += p1[1] * p2[0];
            }

            double answer = (prod1 - prod2) / 2;

            if (polygon[0] == polygon[polygon.Count - 1]) // Deshacemos el loop del polígono (mirar inicio del método)
                polygon.RemoveAt(polygon.Count - 1);

            return Math.Abs(answer); //Devolvemos el valor del área
        }

        //Funcion para encontrar la interseccion de dos rectas
        public static double[] InterseccionVertical(Recta recta1, double[] puntoControl)
        {
            double x = puntoControl[0];
            double y = recta1.a * x + recta1.b;
            double[] solucion = { x, y };

            return solucion;
        }

        public static double[] InterseccionHorizontal(Recta recta1, double[] puntoControl)
        {
            
            double y = puntoControl[1];

            double x = (y - recta1.b) / recta1.a;
            double[] solucion = { x, y };

            return solucion;
        }

    }
}
