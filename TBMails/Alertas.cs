using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TBMails
{
    public class Alertas
    {
        public string Ficheiro {get;set;}
        public string Campo { get; set; }
        public TipoAlerta Alerta { get; set; }
        public DateTime TimeError { get;set; }
      //  public Button Butao { get; set; }
        public string Text { get { return Alerta.ToString()+" "+Ficheiro+" - Falta o campo: "+Campo; } }
        public Alertas() {

            //Butao = new Button();
            //Butao.RenderSize = new System.Windows.Size(50, 40);
                }
        
    }
    public enum TipoAlerta{
        Erro=0,
        Aviso = 1,
        Alerta = 2,
        Valor_Falta = 3,
        Sucesso = 4
    }
}
