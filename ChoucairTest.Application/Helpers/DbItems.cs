namespace ChoucairTest.Application.Helpers;

public static class DbItems
{
    public static string SCHEMA = "[Choucair]";
    public static class StoredProcedures 
    {
        public static readonly string CONSULTA_TAREA_POR_ID = $"{SCHEMA}.TareaOneQuery";

        public static readonly string CONSULTA_TAREAS = $"{SCHEMA}.TareaQuery";
    }
}
