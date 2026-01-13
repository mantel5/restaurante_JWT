using Models;

public class Postre : Producto {

    public int Calorias {get;set;}


public Postre(string nombre, double precio, int calorias): base(nombre, precio) {
   Calorias = calorias;
     if (int.IsNegative(calorias)){
    throw new InvalidIngredientesException("El postre no puede tener calorias negativas");
   }
}

public Postre() { }
public override void MostrarDetalles() {
    Console.WriteLine($"Postre: {Nombre}, Precio {Precio:C}, Calorías {Calorias} ");
}
}