﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.GUI;
using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel.Attributes;
using DcPython.Properties;

namespace DcPython.Decodes {

    public class Decodes_Attributes : GH_Goo<Rhino.DocObjects.ObjectAttributes> {
        public Decodes_Attributes() : base() { this.Value = new Rhino.DocObjects.ObjectAttributes();  }
        public Decodes_Attributes( Decodes_Attributes instance ) { this.Value = instance.Value.Duplicate(); }
        public override IGH_Goo Duplicate() { return new Decodes_Attributes(this); }

        public override bool IsValid { get { return true; } }
        public override object ScriptVariable() { return new Decodes_Attributes(this); }

        public string layer; // must be handled properly when baking
        public string name { get { return Value.Name; } set { Value.Name = value; } }
        public Color color {
            get { return Value.ObjectColor; }
            set {
                Value.ObjectColor = value;
                Value.ColorSource = Rhino.DocObjects.ObjectColorSource.ColorFromObject;
                Value.PlotColor = value;
                Value.PlotColorSource = Rhino.DocObjects.ObjectPlotColorSource.PlotColorFromObject;
            }
        }
        public void setColor( float r, float g, float b ) { this.color = Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));  }
        public void setColor( float a, float r, float g, float b ) { this.color = Color.FromArgb((int)(a * 255),(int)(r * 255), (int)(g * 255), (int)(b * 255)); }

        public double weight { get { return Value.PlotWeight; } set { Value.PlotWeight = value; Value.PlotWeightSource = Rhino.DocObjects.ObjectPlotWeightSource.PlotWeightFromObject; } }

        public void SetSourcesToObject() {
            Value.ColorSource = Rhino.DocObjects.ObjectColorSource.ColorFromObject;
            Value.PlotColorSource = Rhino.DocObjects.ObjectPlotColorSource.PlotColorFromObject;
            Value.PlotWeightSource = Rhino.DocObjects.ObjectPlotWeightSource.PlotWeightFromObject;
        }

        public void SetSourcesToDefault() {
            Rhino.DocObjects.ObjectAttributes defaultAtt = new Rhino.DocObjects.ObjectAttributes();
            Value.ColorSource = defaultAtt.ColorSource;
            Value.PlotColorSource = defaultAtt.PlotColorSource;
            Value.PlotWeightSource = defaultAtt.PlotWeightSource;
        }


        public override string ToString() {
            string ret = "attr[";
            ret += "name="+name;
            if (Value.ColorSource == Rhino.DocObjects.ObjectColorSource.ColorFromObject) ret += "| color=" + color.ToString();
            if (Value.PlotWeightSource == Rhino.DocObjects.ObjectPlotWeightSource.PlotWeightFromObject) ret += "| weight=" + weight;
            if (layer != null) ret += "| layer=" + layer;
            ret += "]";
            return ret;
        }
        public override string TypeDescription { get { return "Represents rhino object attributes."; } }
        public override string TypeName { get { return "Attributes"; } }

        // This function is called when Grasshopper needs to convert other data into this type.
        public override bool CastFrom( object source ) {
            //Abort immediately on bogus data.
            if (source == null) { return false; }
            return false;
        }


    }

    public class GHParam_Decodes_Attributes : GH_Param<Decodes_Attributes> {
        public GHParam_Decodes_Attributes()
            : base(new GH_InstanceDescription("Decodes Object Properties", "props", "Wraps rhino object attributes.", "Maths", "Decodes"))
        {
        }
        public override Grasshopper.Kernel.GH_Exposure Exposure { get { return GH_Exposure.secondary | GH_Exposure.obscure; } }
        public override System.Guid ComponentGuid { get { return new Guid("{B1C480D3-EE0C-4E50-828F-4F3AC6D80639}"); } }

        protected override Bitmap Icon { get { return Resources.Icons_Param_Props; } }

    }




}
