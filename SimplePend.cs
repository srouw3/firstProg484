//============================================================================
// SimplePend.cs Define a class for simulating a simple pendulum
//============================================================================
using System;

namespace Sim
{
    public class SimplePend
    {
        private double len = 1.1; // pendulum length
        private double g = 9.81; // gravitatin field strength
        int n = 2;               // number of states
        private double[] x;      // array of states
        private double[] f;      // right side of equation evaluated

        private double[] k1;
        private double[] k2;
        private double[] k3;
        private double[] k4;
        private double[] w;

        //--------------------------------------------------------------------
        // constructor
        //--------------------------------------------------------------------
        public SimplePend()
        {
            x = new double[n];
            f = new double[n];

            k1 = new double[n];        // holds first slope
            k2 = new double[n];        // holds second slope
            k3 = new double[n];        // holds third slope
            k4 = new double[n];        // holds fourth slope

            w = new double[n];         // holds array of states for RK4

            x[0] = 1.0;
            x[1] = 0.0;
        }

        //--------------------------------------------------------------------
        // step: perform one integration step via Euler's Method 
        //       Soon, it will implement RK4
        //--------------------------------------------------------------------
        public void step(double dt)
        {
            rhsFunc(x,k1);      // evaluate at the first slope's step
            w[0] = x[0]+0.5*dt*k1[1];
            rhsFunc(w,k2);      // evaluate at the second slope's step
            w[0] = x[0]+0.5*dt*k2[1];
            rhsFunc(w,k3);      // evaluate at the third slope's step
            w[0] = x[0]+dt*k3[1];
            rhsFunc(w,k4);      // evaluate at the fourth slope's step

            
            for(int i=0;i<n;++i)
            {
                x[i] = x[i]+(1.0/6.0)*(k1[i]+2*k2[i]+2*k3[i]+k4[i])*dt;     // updated for the RK4 equation
            }
        }

        //--------------------------------------------------------------------
        // rhsFunc: function to calculate rhs of pendulum ODEs
        //--------------------------------------------------------------------
        public void rhsFunc(double[] st, double[] ff)
        {
            ff[0] = st[1];
            ff[1] = -g/len * Math.Sin(st[0]);      
        }

        
        //--------------------------------------------------------------------
        // Getters and setters
        //--------------------------------------------------------------------
        public double L
        {
            get {return (len);}

            set
            {
                if(value > 0.0)
                    len = value;
            }
        }

        public double G
        {
            get {return (g);}

            set
            {
                if(value >= 0.0)
                    g = value;
            }
        }

        public double theta
        {
            get {return x[0];}

            set {x[0] = value;}
        }

        public double thetaDot
        {
            get {return x[1];}

            set {x[1] = value;}
        }
    }
    
}    