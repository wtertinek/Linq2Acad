using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using System;

namespace Linq2Acad
{
  /// <summary>
  /// The main class that provides access to the drawing database.
  /// </summary>
  public sealed class AcadDatabase : AcadDataModel, IDisposable
  {
    private readonly bool disposeDatabase;
    private readonly Action<Database> postProcessCommit;

    private AcadDatabase(Database database, bool keepDatabaseOpen,
                         Action<Database> postProcessCommit = null)
      : base(database, database.TransactionManager.StartTransaction())
    {
      disposeDatabase = !keepDatabaseOpen;
      this.postProcessCommit = postProcessCommit;
    }

    #region Instance methods

    /// <summary>
    /// Immediately discards all changes and aborts the underlying transaction. The session is no longer usable after calling this method.
    /// </summary>
    public void Abort()
    {
      if (!transaction.IsDisposed)
      {
        transaction.Abort();
        transaction.Dispose();
      }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      if (!transaction.IsDisposed)
      {
        Require.NotDisposed(Database.IsDisposed, nameof(AcadDatabase));

        transaction.Commit();

        if (SummaryInfo.Changed)
        {
          SummaryInfo.Commit();
        }

        postProcessCommit?.Invoke(Database);

        transaction.Dispose();
      }

      if (disposeDatabase)
      {
        Database.Dispose();
      }
    }

    private static void SaveDatabaseAs(Database database, string fileName, SaveAsDwgVersion dwgVersion)
    {
      DwgVersion GetDwgVersion()
      {
        switch (dwgVersion)
        {
          case SaveAsDwgVersion.DWG2004:
            return DwgVersion.AC1015;
          case SaveAsDwgVersion.DWG2007:
            return DwgVersion.AC1021;
          case SaveAsDwgVersion.DWG2010:
            return DwgVersion.AC1024;
          case SaveAsDwgVersion.DWG2013:
            return DwgVersion.AC1027;
#if AutoCAD_2018 || AutoCAD_2019 || AutoCAD_2020 || AutoCAD_2021 || AutoCAD_2022 || AutoCAD_2023 || AutoCAD_2024
          case SaveAsDwgVersion.DWG2018:
            return DwgVersion.AC1032;
#endif
          case SaveAsDwgVersion.NewestAvailable:
            return DwgVersion.Newest;
          case SaveAsDwgVersion.DontChange:
          default:
            return database.OriginalFileSavedByVersion;
        }
      }

      if (fileName != null)
      {
        database.SaveAs(fileName, GetDwgVersion());
      }
    }

    #endregion

    #region Factory methods

    /// <summary>
    /// Provides access to a newly created drawing database.
    /// </summary>
    /// <param name="options">Database create options.</param>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Create(CreateOptions options = null)
    {
      options = options ?? new CreateOptions()
                           {
                             SaveDwgVersion = SaveAsDwgVersion.NewestAvailable
                           };

      return new AcadDatabase(new Database(true, true), false,
                              d => SaveDatabaseAs(d, options.SaveFileName, options.SaveDwgVersion));
    }

    /// <summary>
    /// Provides access to the drawing database of the active document.
    /// </summary>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Active()
    {
      Require.ObjectNotNull(Application.DocumentManager.MdiActiveDocument, "No active document");

      return new AcadDatabase(Application.DocumentManager.MdiActiveDocument.Database, true);
    }

    /// <summary>
    /// Provides access to the given drawing database.
    /// </summary>
    /// <param name="database">The drawing database to use.</param>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase Use(Database database)
    {
      Require.ParameterNotNull(database, nameof(database));
      Require.NotDisposed(database.IsDisposed, nameof(Database), nameof(database));

      return new AcadDatabase(database, true);
    }

    /// <summary>
    /// Provides read-only access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="options">Options for opening the database.</param>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase OpenReadOnly(string fileName, OpenReadOnlyOptions options = null)
    {
      Require.StringNotEmpty(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      options = options ?? new OpenReadOnlyOptions();

      var database = GetDatabase(fileName, true, options.Password);
      return new AcadDatabase(database, false);
    }

    /// <summary>
    /// Provides read/write access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="options">Options for opening and closing the database.</param>
    /// <returns>The AcadDatabase instance.</returns>
    public static AcadDatabase OpenForEdit(string fileName, OpenForEditOptions options = null)
    {
      Require.StringNotEmpty(fileName, nameof(fileName));
      Require.FileExists(fileName, nameof(fileName));

      options = options ?? new OpenForEditOptions()
                           {
                             SaveAsFileName = fileName,
                             DwgVersion = SaveAsDwgVersion.DontChange,
                           };

      var database = GetDatabase(fileName, false, options.Password);
      var outFileName = options.SaveAsFileName ?? fileName;
      return new AcadDatabase(database, false,
                              d => SaveDatabaseAs(d, outFileName, options.DwgVersion));
    }

    /// <summary>
    /// Provides access to the drawing database in the given file.
    /// </summary>
    /// <param name="fileName">The name of the drawing database to open.</param>
    /// <param name="readOnly">If true open the drawing in read-only mode. If false, open it in read/write mode.</param>
    /// <param name="password">The password for the drawing database.</param>
    /// <returns>The Autocad Database instance.</returns>
    private static Database GetDatabase(string fileName, bool readOnly, string password)
    {
      var database = new Database(false, true);
      database.ReadDwgFile(fileName, readOnly ? FileOpenMode.OpenForReadAndReadShare : FileOpenMode.OpenForReadAndWriteNoShare, false, password);
      database.CloseInput(true);
      return database;
    }

    #endregion

    #region Overrides to remove methods from IntelliSense

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override bool Equals(object obj)
      => base.Equals(obj);

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override int GetHashCode()
      => base.GetHashCode();

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public override string ToString()
      => base.ToString();

    #endregion
  }
}
