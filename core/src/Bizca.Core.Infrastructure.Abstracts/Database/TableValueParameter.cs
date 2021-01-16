namespace Bizca.Core.Infrastructure.Abstracts.Database
{
    using Dapper;
    using System.Data;
    using System.Data.SqlClient;

    public class TableValueParameter : SqlMapper.ICustomQueryParameter
    {
        private readonly string _typeName;
        private readonly DataTable _dataTable;
        public TableValueParameter(DataTable dataTable)
        {
            _dataTable = dataTable;
            _typeName = _dataTable.TableName;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = (SqlParameter)command.CreateParameter();
            parameter.ParameterName = name;
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.Value = _dataTable;
            parameter.TypeName = _typeName;
            command.Parameters.Add(parameter);
        }
    }
}
