using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using System.Drawing;
using System.Windows.Forms;

namespace MapasEthan2
{
    public struct Recta
    {
        // Campos del struct
        public PointLatLng puntoInicial, puntoFinal;
        public int utmzone;
        public double[] punto1, punto2;
       public double a, b;
        public PointF inicioF;
        public PointF finalF;
        
        public Recta(PointLatLng P1, PointLatLng P2)
        {
            this.puntoInicial = P1;
            this.puntoFinal = P2;
            this.utmzone = Utilidades.ZonaUtm(P1);
            this.punto1 = Utilidades.PasarACartesianas(puntoInicial,utmzone);
            this.punto2 = Utilidades.PasarACartesianas(puntoFinal,utmzone);
            this.a = (punto1[1] - punto2[1]) / (punto1[0] - punto2[0]); 
            this.b = punto1[1] - (a * punto1[0]);

            float x1 = (float)this.punto1[0];
            float y1 = (float)this.punto1[1];
            float x2 = (float)this.punto2[0];
            float y2 = (float)this.punto2[1];

            this.inicioF = new PointF(x1,y1);
            
            this.finalF = new PointF(x2,y2);
          

        
        }




        public Recta(double[] P1, double[] P2, int utmzone) 
        {
            this.punto1 = P1;
            this.punto2 = P2;
            this.utmzone = utmzone;
            this.puntoInicial = Utilidades.PasarAWGS(punto1, utmzone);
            this.puntoFinal = Utilidades.PasarAWGS(punto2, utmzone);
            this.a = (punto1[1] - punto2[1]) / (punto1[0] - punto2[0]); 
            this.b = punto1[1] - (a * punto1[0]);

            float x1 = (float)this.punto1[0];
            float y1 = (float)this.punto1[1];
            float x2 = (float)this.punto2[0];
            float y2 = (float)this.punto2[1];

            this.inicioF = new PointF(x1, y1);

            this.finalF = new PointF(x2, y2);

            //this.inicioF.X = x1;
            //this.inicioF.Y = y1;
            //this.finalF.X = x2;
            //this.finalF.Y = y2;
        }
        

    }
    
}
