namespace ClubBlazor.Class
{
    public static class ApiEndPoints
    {
        public static string Deporte { get; set; } = "deportes";
        public static string Deportista { get; set; } = "deportistas";
        public static string Profesor { get; set; } = "profesores"; // Cambiado de Profesore a Profesor
        public static string Socio { get; set; } = "socios";
        public static string Cuota { get; set; } = "cuotas";

        public static string GetEndPoint(string name)
        {
            return name switch
            {
                nameof(Deporte) => Deporte,
                nameof(Deportista) => Deportista,
                nameof(Profesor) => Profesor, // Actualizado para coincidir con el nuevo nombre
                nameof(Socio) => Socio,
                nameof(Cuota) => Cuota,
                _ => throw new ArgumentException($"EndPoint '{name}' no está definido")
            };
        }
    }
}
