using System;
using LineImpedance;
using LineImpedance.Coaxial;
using LineImpedance.Extensions;

var calculator = new CoaxialCalculator();

var type = typeof(Impedance);

var d1 = type.GetMethod("Coaxial");

var summary = d1.GetXmlSummary();

Console.ReadLine();

