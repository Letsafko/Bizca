namespace Bizca.Core.Infrastructure.Database
{
    using Dapper;
    using System.Data;
    using System.Data.SqlClient;

    public class TableValueParameter : SqlMapper.ICustomQueryParameter
    {
        private readonly string typeName;
        private readonly DataTable dataTable;
        public TableValueParameter(DataTable dataTable)
        {
            this.dataTable = dataTable;
            typeName = this.dataTable.TableName;
        }

        public void AddParameter(IDbCommand command, string name)
        {
            var parameter = (SqlParameter)command.CreateParameter();
            parameter.ParameterName = name;
            parameter.SqlDbType = SqlDbType.Structured;
            parameter.Value = dataTable;
            parameter.TypeName = typeName;
            command.Parameters.Add(parameter);
        }
    }
}