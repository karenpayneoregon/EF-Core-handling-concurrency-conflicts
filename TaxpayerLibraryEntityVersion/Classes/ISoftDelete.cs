namespace TaxpayerLibraryEntityVersion.Classes;

using Data;

/// <summary>
/// Used for soft delete to identify models in override of SaveChanges in <see cref="Context"/>
/// </summary>
public interface ISoftDelete
{
    bool IsDeleted { get; set; }
}