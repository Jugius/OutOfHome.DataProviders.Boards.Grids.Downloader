namespace OutOfHome.DataProviders.Boards.Grids.Downloader.Entities.Oohelp.Common.Enums;
public enum BmaExportMode
{
    None = 0,
    Bigmedia = 1, // Только Бигмедиа
    Partners = 2, // Только партнерка (операторы)
    BigmediaAndBma = 3,
    BigmediaAndPartners = 4
}

public enum OctagonExportMode
{
    None = 0,
    All = 1
}

public enum OuthubExportMode
{
    None = 0, 
    Prime = 1, // Только Прайм (переименовать в ПРАЙМ)
    Outhub = 2, // Прайм + хаб (переименовать в OUTHUB)    
    PrimeAndOuthub = 3, // Прайм + хаб (Прайм => ПРАЙМ, хаб => OUTHUB)
    PrimeAndPartners = 4 // Прайм + хаб (Прайм => ПРАЙМ, хаб => восстановить партнерку)
}
