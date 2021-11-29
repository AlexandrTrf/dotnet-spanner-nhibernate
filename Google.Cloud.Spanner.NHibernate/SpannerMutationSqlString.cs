// Copyright 2021 Google Inc. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using NHibernate.SqlCommand;

namespace Google.Cloud.Spanner.NHibernate
{
    /// <summary>
    /// SqlString implementation that adds additional information to the Sql command
    /// so the command can also (potentially) be executed as a mutation on Spanner.
    /// </summary>
    public class SpannerMutationSqlString : SqlString
    {
        public string MutationCommandText => $"{Operation} {Table}";
        public string Operation { get; }
        public string Table { get; }
        public string[] Columns { get; }
        public int DiscriminatorColumnIndex { get; }
        public string[] WhereColumns { get; }
        public int WhereParamsStartIndex { get; }

        public SpannerMutationSqlString(SqlString dmlSqlString, string operation, string table, string[] columns, int discriminatorColumnIndex) : base(dmlSqlString)
        {
            Operation = operation;
            Table = table;
            Columns = columns;
            DiscriminatorColumnIndex = discriminatorColumnIndex;
            WhereColumns = new string[] { };
        }

        public SpannerMutationSqlString(SqlString dmlSqlString, string operation, string table, string[] columns, string[] whereColumns, int whereParamsStartIndex) : base(dmlSqlString)
        {
            Operation = operation;
            Table = table;
            Columns = columns;
            WhereColumns = whereColumns;
            WhereParamsStartIndex = whereParamsStartIndex;
        }
    }
}