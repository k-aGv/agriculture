﻿/*!
The MIT License (MIT)

Copyright (c) 2017 Dimitris Katikaridis <dkatikaridis@gmail.com>,Giannis Menekses <johnmenex@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;


namespace kagv {
    public partial class gmaps : Form {
        internal readonly GMapOverlay myobjects = new GMapOverlay("objects");

        public gmaps() {
            InitializeComponent();
        }

        private void gmaps_Load(object sender, EventArgs e) {

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.CenterToScreen();

            nud_opacity.Maximum = 255;
            nud_opacity.Minimum = 0;

            //map implementation
            mymap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;//using it as FULL reference to have the complete list of providers
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            mymap.SetPositionByKeywords("greece,thessaloniki");
            mymap.MinZoom = 0;
            mymap.MaxZoom = 18;
            mymap.Zoom = 8;
            mymap.Overlays.Add(myobjects);
            mymap.DragButton = MouseButtons.Left;
            mymap.InvertedMouseWheelZooming = true;

            cb_provider.Items.Add("GoogleMapProvider");
            cb_provider.Items.Add("GoogleTerrainMapProvider");
            cb_provider.Items.Add("BingSatelliteMapProvider");
            cb_provider.Text = "GoogleMapProvider";
            //its not a joke ->
            //____________________________________________________________________opacity______________R___________________________G_______________________B
            mymap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            nud_opacity.Value = 33;

            label1.Location = new Point(10,mymap.Location.X + mymap.Size.Height + 5);

        }



        private void cb_cross_CheckedChanged(object sender, EventArgs e) {
            mymap.ShowCenter = cb_cross.Checked;
            mymap.Refresh();
        }

        private void btn_marker_Click(object sender, EventArgs e) {
            Screenshot st = new Screenshot(this);
            st.Owner = this;
            st.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            mymap.Zoom += 1;
            mymap.Refresh();
        }

        private void mymap_MouseClick(object sender, MouseEventArgs e) {

        }

        private void cb_provider_SelectedIndexChanged(object sender, EventArgs e) {
            if (cb_provider.SelectedItem.ToString() == "GoogleTerrainMapProvider")
                mymap.MapProvider = GMap.NET.MapProviders.GoogleTerrainMapProvider.Instance;
            if (cb_provider.SelectedItem.ToString() == "GoogleMapProvider")
                mymap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            if (cb_provider.SelectedItem.ToString() == "BingSatelliteMapProvider")
                mymap.MapProvider = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;

            mymap.Refresh();
        }

        private void mymap_MouseMove(object sender, MouseEventArgs e) {
            label1.Text = mymap.ViewArea + "";

            label2.Text = "Lat:\r\n" + mymap.ViewArea.Lat + "";
            label3.Text = "Lng:\r\n" + mymap.ViewArea.Lng + "";
            label4.Text = "WidthLng:\r\n" + mymap.ViewArea.WidthLng + "";
            label5.Text = "HeightLat:\r\n" + mymap.ViewArea.HeightLat + "";


            double remoteLat = mymap.FromLocalToLatLng(e.X, e.Y).Lat;
            double remoteLng = mymap.FromLocalToLatLng(e.X, e.Y).Lng;
            label6.Text = "Current coordinates:\r\n" + "X/Lat:" + remoteLat + "\r\n" + "Y/Lng:" + remoteLng;
        }

        private void cb_wheel_CheckedChanged(object sender, EventArgs e) {
            mymap.InvertedMouseWheelZooming = cb_wheel.Checked;
            mymap.Refresh();
        }

        private void btn_color_Click(object sender, EventArgs e) {
            if (cd.ShowDialog() == DialogResult.OK) {
                mymap.SelectedAreaFillColor = Color.FromArgb((int)nud_opacity.Value, cd.Color);

                mymap.Refresh();
            }
        }

        private void nud_opacity_ValueChanged(object sender, EventArgs e) {
            mymap.SelectedAreaFillColor = Color.FromArgb((int)nud_opacity.Value, mymap.SelectedAreaFillColor);
            mymap.Refresh();

        }




    }
}
