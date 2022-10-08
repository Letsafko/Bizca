namespace Bizca.Core.Infrastructure.Database
{
    using Dapper;
    using System.Data;
    using System.Data.SqlClient;

    public class TableValueParameter : SqlMapper.ICustomQueryParameter
    {
        private readonly DataTable _dataTable;
        private readonly string _typeName;

        public TableValueParameter(DataTable dataTable)
        {
            _typeName = dataTable.TableName;
            _dataTable = dataTable;
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