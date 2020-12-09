using System;

[AttributeUsage(AttributeTargets.Class)]
public class CSingletonAttribute : Attribute
{
    private readonly string name;
    private readonly bool persistant;
    private readonly int[] banedSceneIndexes;
    private readonly string[] banedSceneNames;

    public string Name
    {
        get { return this.name; }
    }

    public bool Persistant
    {
        get { return this.persistant; }
    }

    public int[] BanedSceneIndexes
    {
        get { return this.banedSceneIndexes; }
    }

    public string[] BanedSceneNames
    {
        get { return this.banedSceneNames; }
    }

    public CSingletonAttribute(bool pPersistant)
    {
        this.persistant = pPersistant;
    }

    public CSingletonAttribute(string pName, bool pPersistant, int[] pBanedSceneIndexes = null, string[] pBanedSceneNames = null)
    {
        this.name = pName;
        this.persistant = pPersistant;
        this.banedSceneIndexes = pBanedSceneIndexes;
        this.banedSceneNames = pBanedSceneNames;
    }
}