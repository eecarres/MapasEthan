using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.Internals;
// Para los cambios de coordenadas del Cálculo de área del polígono
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;


namespace MapasEthan2
{
    public partial class Form1 : Form
    {
        
        
        // Creamos los contenedores de todas las overlays de markers y polígonos
        public List<GMapOverlay> listaOverlaysMarkers = new List<GMapOverlay>();
        public List<GMapOverlay> listaOverlaysPolygons = new List<GMapOverlay>();
        public List<GMapPolygon> listaPoligonos = new List<GMapPolygon>();
        int numOverMarkers=0;
        int numOverPolygons = 0;

        bool poligonMode = false;
        bool modoRecta = false;

        Recta rectaDivisoria;

        // Puntos de la recta de division

        PointLatLng puntoRecta1 = new PointLatLng(0.0, 0.0);
        PointLatLng puntoRecta2 = new PointLatLng(0.0, 0.0);
        
        //Generamos la overlay que usaremos para los puntos, y la lista de puntos que contiene (puntos en formato PointLatLng)

        public static List<PointLatLng> puntosMarkers = new List<PointLatLng>();
       public static List<GMapMarker> markers = new List<GMapMarker>();

        // Creamos los métodos que generan una nueva overlay de polígonos y  markers (para cuando entramos en modo polígono
        public void CreaOverlayMarkers() 
        {
            numOverMarkers++;// Añadimos una al contador
            GMapOverlay  overlayMarkers= new GMapOverlay(); // Creamos la nueva overlay de markers
            listaOverlaysMarkers.Add(overlayMarkers); // Añadimos la overlay a la lista correspondiente
            gmap.Overlays.Add(listaOverlaysMarkers[numOverMarkers-1]); // Añadimos la overlay al mapa principal
        }
        public void CreaOverlayPolygons()
        {
            numOverPolygons++; // Añadimos una al contador
            GMapOverlay overlayPolygons = new GMapOverlay(); // Creamos la nueva overlay de markers
            listaOverlaysPolygons.Add(overlayPolygons); // Añadimos la overlay a la lista correspondiente
            gmap.Overlays.Add(overlayPolygons);
        }
        public void CreaPoligono() {
            GMapPolygon poligono = new GMapPolygon(puntosMarkers,numOverPolygons.ToString());
            listaPoligonos.Add(poligono);
            listaOverlaysPolygons[numOverPolygons - 1].Polygons.Add(poligono);
            poligono.Fill = new SolidBrush(Color.FromArgb(25, Color.DarkRed));
            poligono.Stroke = new Pen(Color.Black, 1);
            
        }

        // Método que escriba un marker y un punto de polígono en la overlay actual

        public void GeneraPoligono( GMapOverlay overlayMarkers, GMapOverlay overlayPolygon, List<GMapMarker> listaMarkers, List<PointLatLng> listaPuntos, Point puntoPantalla, GMapPolygon poligono, Control label)
        {
            
                
           
            // Se añade el punto del Mapa, que es la posicion en relativas pasada a la posicion del mapa
            PointLatLng puntoMapa = gmap.FromLocalToLatLng(puntoPantalla.X, puntoPantalla.Y);
            // Se añade un punto a la lista de puntos de marcadores
            listaPuntos.Add(puntoMapa);
            // Se añade el punto al polígono
            poligono.Points.Add(puntoMapa);
            //Se genera el marcador en el punto y se añade el marcador al overlay
            Bitmap hm = new Bitmap(global::MapasEthan2.Properties.Resources.HemavMarker);
            GMarkerGoogle marker = new GMarkerGoogle(puntoMapa, hm);
            overlayMarkers.Markers.Add(marker);
            markers.Add(marker);
            if (listaPuntos.Count > 2)
            {
                double areaPoligono = Utilidades.calcpolygonarea(listaPuntos) / 10000;
                string textoArea = String.Format("{0:0.00}", areaPoligono);
                label.Text = (textoArea + " ha");
                //gmap.Overlays.Add(listaOverlaysPolygons[numOverPolygons - 1]); // Añadimos la overlay al mapa principal
            }
            // Se actualizan el marker, el polígono y el mapa
            //gmap.Overlays.Add(listaOverlaysPolygons[numOverPolygons - 1]);
            gmap.UpdatePolygonLocalPosition(poligono);
            gmap.UpdateMarkerLocalPosition(marker);
            gmap.Refresh();
            
        }

        // Campos para el movimiento de los marcadores
        // Lo que el programa hace será guardar el punto inicial cuando el mouse baje (todo esto dentro del evento MouseMove) y 
        // cuando suba obtendremos el punto. Ese sera el punto que guardaremos en la localizacion del marker, cambiando el inicial que teníamos. Para saber que marker es
        // compararemos la posicion inicial con la de todos los markers de la overlay actual de markers.
        public PointLatLng puntoInicial;
        public PointLatLng puntoFinal;
        public bool mouseArrastrando = false;

        // Métodos para guardar POSICION del mouse
        public void GuardaPosInicial()
        {
            Point puntoActual = new Point(MousePosition.X, MousePosition.Y);
            puntoActual = gmap.PointToClient(puntoActual);
            puntoInicial = gmap.FromLocalToLatLng(puntoActual.X,puntoActual.Y);
        }

        public void GuardaPosFinal()
        {
            Point puntoActual = new Point(MousePosition.X, MousePosition.Y);
            puntoActual = gmap.PointToClient(puntoActual);
            puntoFinal = gmap.FromLocalToLatLng(puntoActual.X, puntoActual.Y);
        }

        public void ReescribirMarker()
        {
            for (int i = 0; i < puntosMarkers.Count; i++)
            {
                if (puntosMarkers[i]== puntoInicial)
                {
                    puntosMarkers[i] = puntoFinal;
                    MessageBox.Show("se supone que he movido un marker");
                }
            }
        }

        public Form1() // Constructor de la clase
        {
            InitializeComponent();
            
        }

        

        public void Form1_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance; // Seleccionamos el proveedor de mapas
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache; // Seleccionamos si guardamos caché o solo online
            gmap.Zoom=16;
            gmap.MaxZoom = 25;
            gmap.Position = new PointLatLng(41.4037486430615, 2.02977776527405); // Seleccionamos la posición que queremos que apunte en el inicio
            
            

            
            gmap.DragButton= MouseButtons.Left; // Cambiamos el boton de arrastrar al izquierdo (el derecho siempre funciona, de todos modos

            CreaOverlayMarkers();
            CreaOverlayPolygons();
            CreaPoligono();

            
            // Creamos un marcador= damos una posición y el tipo de marcador que queremos usar, en este caso el de Hemav
            //PointLatLngAlt posicionInicial = new PointLatLngAlt(41.4037486430615, 2.02977776527405);
            //GMarkerGoogle marcador = new GMarkerGoogle(posicionInicial, new Bitmap("C:\\Users\\Ethan\\Universidad\\PFC\\C#\\MapasEthan2\\MapasEthan2\\HemavMarker.png"));
            //listaOverlaysMarkers[numOverMarkers - 1].Markers.Add(marcador);

            //double[] puntoUTM;
            //puntoUTM = posicionInicial.ToUTM();
            //puntoUTM[0] += 100;
            //PointLatLngAlt nuevaPosicion = new PointLatLngAlt();
            //nuevaPosicion = Utilidades.PasarAWGS(puntoUTM, 31);
            //GMarkerGoogle marcadorNuevo = new GMarkerGoogle(nuevaPosicion, new Bitmap("C:\\Users\\Ethan\\Universidad\\PFC\\C#\\MapasEthan2\\MapasEthan2\\HemavMarker.png"));
            //listaOverlaysMarkers[numOverMarkers - 1].Markers.Add(marcadorNuevo);

            //puntoUTM = Utilidades.PasarACartesianas(nuevaPosicion, 31);
            ////puntoUTM[1] += 100000;
            //nuevaPosicion = Utilidades.PasarAWGS(puntoUTM, 31);
            //GMarkerGoogle marcador3 = new GMarkerGoogle(nuevaPosicion, new Bitmap("C:\\Users\\Ethan\\Universidad\\PFC\\C#\\MapasEthan2\\MapasEthan2\\HemavMarker.png"));
            //listaOverlaysMarkers[numOverMarkers - 1].Markers.Add(marcador3);


            //gmap.Overlays.Add(listaOverlaysMarkers[numOverMarkers - 1]);
             // Se añade el overlay al mapa (se dibuja)
            //gmap.UpdateMarkerLocalPosition(marcador);
            //GMapOverlay poligonos = new GMapOverlay("poligonos"); // Generamos el overlay de poligonos
            //List <PointLatLng> puntosP1 = new List<PointLatLng>(); // Usamos un tipo LIST para los puntos, de tipo PointLatLng (atención a la sintaxis de llamada de list)
            // Creamos los tres puntos que forman el polígono, haciendo un NOMBRELISTA.ADD(PUNTO DEFINIDO O DEFINICIÓN DE UNO NUEVO)
            //puntosP1.Add(new PointLatLng (41.4037486430615, 2.02977776527405));
            //puntosP1.Add(new PointLatLng(42.4037486430615, 2.02977776527405));
            //puntosP1.Add(new PointLatLng(41.4037486430615, 1.02977776527405));
            //Creamos el polígono dandole como referencia la lista de puntos
            //GMapPolygon poligono = new GMapPolygon(puntosP1, "primer poligono");
            // PIntamos el polígono por dentro y luego damos el formato a la línea que lo define
            //poligono.Fill = new SolidBrush(Color.FromArgb(25,Color.DarkRed));
            //poligono.Stroke = new Pen(Color.Black, 1);
            // Creamos un segundo polígono con la segunda lista de puntos PointLatLng, definimos los puntos, el polígono y los colores
            //List<PointLatLng> puntosP2 = new List<PointLatLng>();
            //puntosP2.Add(new PointLatLng(41.4037486430615, 2.02977776527405));
            //puntosP2.Add(new PointLatLng(42.4037486430615, 3.02977776527405));
            //puntosP2.Add(new PointLatLng(42.4037486430615, 1.02977776527405));
            //GMapPolygon poligono2 = new GMapPolygon(puntosP2, "segundo poligono");
            //poligono2.Fill = new SolidBrush(Color.FromArgb(25, Color.DarkBlue));
            //poligono2.Stroke = new Pen(Color.DarkRed, 1);

            // Se añaden los polígonos a la overlay que hemos creado anteriormente, y luego se añade la overlay al mapa
            //poligonos.Polygons.Add(poligono);
            //poligonos.Polygons.Add(poligono2);
            //gmap.Overlays.Add(poligonos);

        }
       
        // Creamos la acción que ocurrirá al entrar en el modo polígono. 
        // En caso de no estar en modo polígono, entraremos y cambia la etiqueta, y al contrario
        private void modoPoligonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (poligonMode==false)
            {
                MessageBox.Show("Has entrado en el modo Polígono");
            poligonMode = true;
            modoPoligonoToolStripMenuItem.Text = "Salir de Modo Polígono";
            dividirPoligonoToolStripMenuItem.Enabled = true;

                //Inicializamos una nueva overlay de polugonos y otra de markers:
            
                CreaOverlayMarkers();
                CreaOverlayPolygons();
                CreaPoligono();
            
                
            }
            else 
            {
                MessageBox.Show("Has salido del modo Polígono");
                poligonMode = false;
                modoPoligonoToolStripMenuItem.Text = "Modo Polígono";
                dividirPoligonoToolStripMenuItem.Enabled = false;
                puntosMarkers.Clear();
            }
        }
    

        // método que tiene que añadir al último polígono un punto y generarlo todo de nuevo
        private void gmap_MouseClick(object sender, MouseEventArgs e)
        {
            if (poligonMode)
            {

                // Generamos el punto referenciado a la pantalla de la aplicacion
                Point puntoPantalla = new Point(Cursor.Position.X, Cursor.Position.Y); // puntoPantalla es el punto en absolutas
                puntoPantalla = gmap.PointToClient(puntoPantalla); // lo pasamos a relativas con el metodo PointToClient

                GeneraPoligono(listaOverlaysMarkers[numOverMarkers - 1], listaOverlaysPolygons[numOverPolygons - 1], markers, puntosMarkers, puntoPantalla, listaPoligonos[numOverPolygons - 1], lbl_Area);

            }

            if (modoRecta) // Generamos la recta necesaria para dividir el polígono
            {
                Point puntoPantalla = new Point(Cursor.Position.X, Cursor.Position.Y); // puntoPantalla es el punto en absolutas
                puntoPantalla = gmap.PointToClient(puntoPantalla);
                if (puntoRecta1.Lat == 0.0)
                {
                    puntoRecta1 = gmap.FromLocalToLatLng(puntoPantalla.X, puntoPantalla.Y);

                }
                else
                {
                    puntoRecta2 = gmap.FromLocalToLatLng(puntoPantalla.X, puntoPantalla.Y);

                    modoRecta = false;

                    // Dibujamos la recta y sus dos puntos en el mapa
                    GMapOverlay overlayRecta = new GMapOverlay();
                    GMarkerGoogle markerRecta1 = new GMarkerGoogle(puntoRecta1, GMarkerGoogleType.red);
                    GMarkerGoogle markerRecta2 = new GMarkerGoogle(puntoRecta2, GMarkerGoogleType.green);
                    overlayRecta.Markers.Add(markerRecta1);
                    overlayRecta.Markers.Add(markerRecta2);
                    gmap.Overlays.Add(overlayRecta);
                    gmap.UpdateMarkerLocalPosition(markerRecta1);
                    gmap.UpdateMarkerLocalPosition(markerRecta2);
                    List<PointLatLng> puntosRuta = new List<PointLatLng>();
                    puntosRuta.Add(puntoRecta1);
                    puntosRuta.Add(puntoRecta2);
                    GMapRoute Ruta = new GMapRoute(puntosRuta, "ruta");

                    GMapOverlay overlayRuta = new GMapOverlay();
                    overlayRuta.Routes.Add(Ruta);

                    gmap.Overlays.Add(overlayRuta);

                    rectaDivisoria = new Recta(puntoRecta1, puntoRecta2);

                    gmap.Refresh();
                    MessageBox.Show("Recta definida");

                    // Hay que encontrar los dos polígonos y enviar cada uno al divisor

                    List<List<PointLatLng>> resultado = DivisorRecta.Separacion(puntosMarkers, rectaDivisoria, listaPoligonos);




                    gmap.Refresh();

                    // Pedimos al usuario cómo quiere dividir el área del primer polígono
                    PreferenciasArea formPoli1 = new PreferenciasArea();
                    formPoli1.Text = "Propiedades polígono 1";
                    formPoli1.ShowDialog();
                    switch (formPoli1.opcion)
                    {
                        case 0: DivisionPoligono.divisionVertical(resultado[0], listaPoligonos, formPoli1.areaMaxima, formPoli1.desplazamientoMaximo);
                            break;
                        case 1: DivisionPoligono.divisionHorizontal(resultado[0], listaPoligonos, formPoli1.areaMaxima, formPoli1.desplazamientoMaximo);
                            break;
                        case 2: DivisionPoligono.divisionVertical(resultado[0], listaPoligonos, formPoli1.areaMaxima, formPoli1.desplazamientoMaximo, formPoli1.franjas);
                            break;
                        case 3: DivisionPoligono.divisionHorizontal(resultado[0], listaPoligonos, formPoli1.areaMaxima, formPoli1.desplazamientoMaximo, formPoli1.franjas);
                            break;
                    }

                    // Pedimos al usuario cómo quiere dividir el área del segundo polígono
                    PreferenciasArea formPoli2 = new PreferenciasArea();
                    formPoli2.Text = "Propiedades polígono 2";
                    formPoli2.ShowDialog();
                    switch (formPoli2.opcion)
                    {
                        case 0: DivisionPoligono.divisionVertical(resultado[1], listaPoligonos, formPoli2.areaMaxima, formPoli2.desplazamientoMaximo);
                            break;
                        case 1: DivisionPoligono.divisionHorizontal(resultado[1], listaPoligonos, formPoli2.areaMaxima, formPoli2.desplazamientoMaximo);
                            break;
                        case 2: DivisionPoligono.divisionVertical(resultado[1], listaPoligonos, formPoli2.areaMaxima, formPoli2.desplazamientoMaximo, formPoli2.franjas);
                            break;
                        case 3: DivisionPoligono.divisionHorizontal(resultado[1], listaPoligonos, formPoli2.areaMaxima, formPoli2.desplazamientoMaximo, formPoli2.franjas);
                            break;
                    }

                    //Borramos la overlay actual
                    gmap.Overlays.Clear();
                    GMapOverlay overlayPoli = new GMapOverlay();
                    for (int i = 1; i < listaPoligonos.Count; i++)
                    {
                        gmap.Overlays.Clear();
                        overlayPoli.Polygons.Add(listaPoligonos[i]);
                        gmap.Overlays.Add(overlayPoli);

                        gmap.Refresh();

                    }

                }

            }
        }



       

       private void numeroDePuntosMarkersToolStripMenuItem_Click(object sender, EventArgs e)
       {
           for (int i = 0; i < (puntosMarkers.Count); i++)
           {
               MessageBox.Show("Hay" + puntosMarkers[i] + " Puntos");
           }
       }

       

       private void volverAVerMarkersToolStripMenuItem_Click(object sender, EventArgs e)
       {
           
       }



        // Checkers para mostrar o no los overlays del polígono y los markers
       private void checkBox1_CheckedChanged(object sender, EventArgs e)
       {

           switch (checkBox1.CheckState)
           {
               case CheckState.Checked:
                   gmap.Overlays.Add(listaOverlaysMarkers[numOverMarkers - 1]);
                   gmap.Refresh();
                   break;
               case CheckState.Unchecked:
                   gmap.Overlays.Remove(listaOverlaysMarkers[numOverMarkers - 1]);
                   gmap.Refresh();
                   break;
           }
       }

       private void checkBox2_CheckedChanged(object sender, EventArgs e)
       {
           switch (checkBox2.CheckState)
           {
               case CheckState.Checked:
                   gmap.Overlays.Add(listaOverlaysPolygons[numOverPolygons - 1]);
                   gmap.Refresh();
                   break;
               case CheckState.Unchecked:
                   gmap.Overlays.Remove(listaOverlaysPolygons[numOverPolygons - 1]);
                   gmap.Refresh();
                   break;
           }
       }

       private void gmap_MouseMove(object sender, MouseEventArgs e)
       {

           
       }

       private void borrarPolígonoToolStripMenuItem_Click(object sender, EventArgs e) // Borramos el polígono
       {
           listaPoligonos[numOverPolygons - 1].Points.Clear();
           listaOverlaysMarkers[numOverMarkers - 1].Markers.Clear();
           puntosMarkers.Clear();
           gmap.Refresh();
       }

       private void gmap_MouseDown(object sender, MouseEventArgs e)
       {
           mouseArrastrando = true;
           GuardaPosInicial();
       }

       private void numeroOverlaysToolStripMenuItem_Click(object sender, EventArgs e)
       {
           MessageBox.Show("Hay " + gmap.Overlays.Count + " Overlays, " + listaOverlaysMarkers.Count+" overlays de markers y "+ listaOverlaysPolygons.Count+"overlays de poligonos");
           
       }

       private void gmap_MouseUp(object sender, MouseEventArgs e)
       {
           if (mouseArrastrando && poligonMode)
           {
               GuardaPosFinal();
               //ReescribirMarker();
               
           }
           mouseArrastrando = false;
          
       }

       

       private void dividirPoligonoToolStripMenuItem_Click(object sender, EventArgs e)
       {
           
       }

       private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
       {

       }

       private void toolStripMenuItem2_Click(object sender, EventArgs e)
       {
           if (poligonMode)
           {
               poligonMode = false;
               modoPoligonoToolStripMenuItem.Text = "Modo Polígono";
               // Creamos el formulario que nos pide los datos

               PreferenciasArea formulario = new PreferenciasArea();
               formulario.ShowDialog();
               switch (formulario.opcion)
               {
                   case 0: DivisionPoligono.divisionVertical(puntosMarkers, listaPoligonos, formulario.areaMaxima, formulario.desplazamientoMaximo);
                       break;
                   case 1: DivisionPoligono.divisionHorizontal(puntosMarkers, listaPoligonos, formulario.areaMaxima, formulario.desplazamientoMaximo);
                       break;
                   case 2: DivisionPoligono.divisionVertical(puntosMarkers, listaPoligonos, formulario.areaMaxima, formulario.desplazamientoMaximo, formulario.franjas);
                       break;
                   case 3: DivisionPoligono.divisionHorizontal(puntosMarkers, listaPoligonos, formulario.areaMaxima, formulario.desplazamientoMaximo, formulario.franjas);
                       break;
               }

               //Borramos la overlay actual
               gmap.Overlays.Clear();
               GMapOverlay overlayPoligonos = new GMapOverlay();
               for (int i = 1; i < listaPoligonos.Count; i++)
               {
                   gmap.Overlays.Clear();
                   overlayPoligonos.Polygons.Add(listaPoligonos[i]);
                   gmap.Overlays.Add(overlayPoligonos);

                   gmap.Refresh();

                   //MessageBox.Show("El polígono " + (i ));
               }
               puntosMarkers.Clear();
           }
           else
           {
               MessageBox.Show("Define primero un polígono");
           }
       }

       private void divisiónConRectaToolStripMenuItem_Click(object sender, EventArgs e)
       {
           if (modoRecta==false)
           {
               poligonMode = false;
               modoPoligonoToolStripMenuItem.Text = "Modo Polígono";
               modoRecta = true;
               MessageBox.Show("Define la recta: click en sus dos puntos");
           }
           
       }

       private void gmap_KeyDown(object sender, KeyEventArgs e)
       {
           
       }

       private void toolStripMenuItem1_Click(object sender, EventArgs e)
       {
           Application.Restart();

       }

       

    }
}
