namespace TestTemplate16.Common.Base;

public interface IVersionedEntity
{
    byte[] RowVersion { get; }

    void SetRowVersion(byte[] rowVersion);
}
