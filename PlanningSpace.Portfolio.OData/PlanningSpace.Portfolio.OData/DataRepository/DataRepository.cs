using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Palantir.Common;
using Palantir.Plan;
using Palantir.Plan.Common;
using Palantir.Plan.Common.Configuration;
using PlanningSpace.Portfolio.OData.Models;

namespace PlanningSpace.Portfolio.OData.DataRepository
{
    public class DataRepository
    {
        #region table and column names 

        private const int MaxNumberOfPeriodicVariablesInCommand = 960; //80 years
        public const string DataSourceGroupsTableName = "DataSourceGroups";
        private const string DataSourceGroupIdColumnName = "DataSourceGroupID";

        public const string DataSourcesTableName = "DataSources";
        private const string DataSourceIdColumnName = "DataSourceID";
        private const string DataSourceGuidColumnName = "DataSourceGUID";
        private const string NameColumnName = "Name";
        private const string DataSourceTypeColumnName = "DataSourceType";
        private const string ConfigColumnName = "Config";

        public const string PriceScenariosTableName = "PriceScenarios";
        private const string PriceScenarioIdColumnName = "PriceScenarioID";

        public const string PriceScenarioWeightingsTableName = "PriceScenarioWeightings";
        private const string WeightingColumnName = "Weighting";
        private const string ProjectIdColumnName = "ProjectID";

        public const string ProjectScenariosTableName = "ProjectScenarios";
        private const string ProjectScenarioIdColumnName = "ProjectScenarioID";

        public const string ProjectScenarioWeightingsTableName = "ProjectScenarioWeightings";
        private const string IsFailureColumnName = "IsFailure";

        public const string VariablesTableName = "Variables";
        private const string VariableIdColumnName = "VariableID";
        private const string VariableTypeColumnName = "VariableType";
        private const string DataTypeColumnName = "DataType";
        private const string IsCurrencyColumnName = "IsCurrency";
        private const string UnitLabelColumnName = "UnitLabel";
        private const string DecimalPlacesColumnName = "DecimalPlaces";
        private const string VisibilityColumnName = "Visibility";
        private const string IsTargetColumnName = "IsTarget";
        private const string TargetTypeColumnName = "TargetType";
        private const string AggregateResultsColumnName = "AggregateResults";
        private const string ExpressionColumnName = "Expression";
        private const string CalculateAllProjectsColumnName = "CalculateAllProjects";
        private const string AggregationMethodColumnName = "AggregationMethod";
        private const string RollupMethodColumnName = "RollupMethod";
        private const string IgnoreShiftColumnName = "IgnoreShift";
        private const string IgnoreWiColumnName = "IgnoreWI";
        private const string IgnorePosColumnName = "IgnorePOS";
        private const string IsSummaryColumnName = "IsSummary";
        private const string IsLinkedColumnName = "IsLinked";

        public const string VariableDataTableName = "VariableData";
        private const string DataColumnName = "Data";
        public const string PeriodicDataColumnName = "PeriodicData";

        public const string ProjectTypesTableName = "ProjectTypes";
        private const string ProjectTypeIdColumnName = "ProjectTypeID";
        private const string DisplayColorColumnName = "DisplayColor";
        private const string CanDivestColumnName = "CanDivest";
        private const string CanShiftColumnName = "CanShift";
        private const string CanChangeWiColumnName = "CanChangeWI";
        private const string CanChangePosColumnName = "CanChangePOS";
        private const string CanBeSurrogateColumnName = "CanBeSurrogate";
        private const string OrderIdColumnName = "OrderId";

        public const string ProjectsTableName = "Projects";
        private const string IsFromDataSourceGroupColumnName = "IsFromDataSourceGroup";
        private const string ProjectDataFolderNameColumnName = "ProjectDataFolderName";
        private const string IncludeColumnName = "Include";
        private const string IncludeOriginalColumnName = "IncludeOriginal";
        private const string ShiftValueColumnName = "ShiftValue";
        private const string ShiftOriginalColumnName = "ShiftOriginal";
        private const string ShiftMinColumnName = "ShiftMin";
        private const string ShiftMaxColumnName = "ShiftMax";
        private const string WiValueColumnName = "WIValue";
        private const string WiMinColumnName = "WIMin";
        private const string WiMaxColumnName = "WIMax";
        private const string WiOriginalColumnName = "WIOriginal";
        private const string PosValueColumnName = "POSValue";
        private const string PosMinColumnName = "POSMin";
        private const string PosMaxColumnName = "POSMax";
        private const string PosOriginalColumnName = "POSOriginal";
        private const string StartYearColumnName = "StartYear";
        private const string DurationColumnName = "Duration";
        private const string PartnerNameColumnName = "PartnerName";
        private const string CustomColorArgbColumnName = "CustomColorArgb";
        private const string EconomicLimitColumnName = "EconomicLimit";
        private const string IsSurrogateColumnName = "IsSurrogate";
        private const string SurrogateStepSizeColumnName = "SurrogateStepSize";
        private const string SurrogateMinTotalColumnName = "SurrogateMinTotal";
        private const string SurrogateMaxTotalColumnName = "SurrogateMaxTotal";
        private const string MutuallyExclusiveGroupColumnName = "MutuallyExclusiveGroup";

        private const string ShiftDependencyColumnName = "ShiftDependency";
        private const string ShiftOptionColumnName = "ShiftOption";
        private const string ShiftValue1ColumnName = "ShiftValue1";
        private const string ShiftValue2ColumnName = "ShiftValue2";
        private const string ShiftParentDateOptionColumnName = "ShiftParentDateOption";
        private const string ParentProjectIdColumnName = "ParentProjectID";
        private const string IncludeDependencyColumnName = "IncludeDependency";
        private const string IsChildIndependentColumnName = "IsChildIndependent";
        private const string WiDependencyColumnName = "WIDependency";
        private const string PosDependencyColumnName = "POSDependency";
        private const string DependencyDisplayColorColumnName = "DependencyDisplayColor";

        public const string ProjectPropertiesTableName = "ProjectProperties";
        private const string ProjectPropertyIdColumnName = "ProjectPropertyID";
        private const string ValueTypeColumnName = "ValueType";
        private const string InSnapshotColumnName = "InSnapshot";

        public const string PropertyValuesTableName = "PropertyValues";
        private const string ValueColumnName = "Value";

        public const string GlobalValuesTableName = "GlobalValues";

        public const string TargetVariablesTableName = "TargetVariables";
        private const string MinValueColumnName = "MinValue";
        private const string MaxValueColumnName = "MaxValue";

        public const string SnapshotsTableName = "Snapshots";
        private const string SnapshotIdColumnName = "SnapshotID";
        private const string PathColumnName = "Path";
        private const string DescriptionColumnName = "Description";
        private const string IsDailyShiftColumnName = "IsDailyShift";

        public const string SnapshotItemsTableName = "SnapshotItems";
        private const string SnapshotItemIdColumnName = "SnapshotItemID";
        private const string SurrogateStepColumnName = "SurrogateStep";
        private const string MinimumTotalAnalogueInstancesColumnName = "MinimumTotalAnalogueInstances";
        private const string MaximumTotalAnalogueInstancesColumnName = "MaximumTotalAnalogueInstances";
        private const string ParentProjectDataSourceNameColumnName = "ParentProjectDataSourceName";
        private const string ParentProjectNameColumnName = "ParentProjectName";
        private const string ProjectTypeColumnName = "ProjectTypeName";

        public const string SnapshotPropertiesTableName = "SnapshotProperties";

        public const string SettingsTableName = "Settings";
        private const string SettingIdColumnName = "SettingID";
        private const string SettingNameColumnName = "SettingName";
        private const string IsDisplayColumnName = "IsDisplay";

        public const string LogItemsTableName = "LogItems";
        private const string DataSourceNameColumnName = "DataSourceName";
        private const string GroupNameColumnName = "GroupName";
        private const string ProjectNameColumnName = "ProjectName";
        private const string MessageColumnName = "Message";
        private const string TimestampColumnName = "Timestamp";
        private const string StackTraceColumnName = "StackTrace";
        private const string LevelColumnName = "Level";

        public const string GroupSummaryValuesTableName = "GroupSummaryValues";

        public const string RulesTableName = "Rules";
        private const string RuleIDColumnName = "RuleID";
        private const string RuleNameColumnName = "RuleName";
        private const string RuleTypeColumnName = "RuleType";
        private const string RuleDataColumnName = "RuleData";

        public const string ExpectedDataTableName = "ExpectedData";

        public const string ProjectPropertyDisplaySettingsTableName = "ProjectPropertyDisplaySettings";
        private const string PropertyNameColumnName = "PropertyName";
        private const string PropertyValueColumnName = "PropertyValue";
        private const string ColorColumnName = "Color";

        public const string PartnerDisplayColorsTableName = "PartnerDisplayColors";

        public const string PriceScenarioDisplayColorsTableName = "PriceScenarioDisplayColors";

        public const string ProjectViewGroupSummaryVariableValuesTableName = "ProjectViewGroupSummaryVariableValues";
        private const string SummaryVariableNameColumnName = "SummaryVariableName";
        private const string GroupRowHandleColumnName = "GroupRowHandle";

        public const string SurrogatesTableName = "Surrogates";
        private const string InstancesColumnName = "Instances";
        private const string MaxInstancesColumnName = "MaxInstances";
        private const string MinInstancesColumnName = "MinInstances";

        public const string SnapshotSurrogatesTableName = "SnapshotSurrogates";

        private const string SnapshotItemsTemporaryTableName = "SnapshotItems_Temp";
        private const string SnapshotPropertiesTemporaryTableName = "SnapshotProperties_Temp";
        private const string SnapshotSurrogatesTemporaryTableName = "SnapshotSurrogates_Temp";

        #endregion

        private static DataRepository _Instance;

        public static DataRepository Instance
        {
            get
            {
                return _Instance ?? (_Instance = new DataRepository());
            }
        }

        private DataRepository()
        {
            _SqliteConnection = SQLiteHelper.CreateConnection(@"C:\Users\jzhao\Documents\Visual Studio 2015\Projects\PlanningSpace.Portfolio.OData\PlanningSpace.Portfolio.OData\PlanData.pdata");
            _SqliteConnection.Open();
        }

        private readonly SQLiteConnection _SqliteConnection;
        private List<Project> _Projects; 
        private PlanProjectTypeCollection GetAllProjectTypes()
        {
            var queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendFormat("SELECT {0},{1},{2},{3},{4},{5},{6},{7},{8} FROM {9}",
            ProjectTypeIdColumnName,
            NameColumnName,
            DisplayColorColumnName,
            CanDivestColumnName,
            CanShiftColumnName,
            CanChangeWiColumnName,
            CanChangePosColumnName,
            CanBeSurrogateColumnName,
            OrderIdColumnName,
            ProjectTypesTableName);

            var allData = new PlanProjectTypeCollection();
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteSQLiteReader(queryStringBuilder.ToString(), _SqliteConnection))
            {
                while (reader.Read())
                {
                    var projectTypeId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    var color = Color.FromArgb(reader.GetInt32(2));
                    var canDivest = reader.GetBoolean(3);
                    var canShift = reader.GetBoolean(4);
                    var canChangeWI = reader.GetBoolean(5);
                    var canChangePOS = reader.GetBoolean(6);
                    var canBeSurrogate = reader.GetBoolean(7);
                    var orderId = reader.GetByte(8);

                    var projectType = new PlanProjectType(name, color, canDivest, canShift, canChangeWI, canChangePOS, canBeSurrogate, orderId);
                    projectType.Id = projectTypeId;
                    projectType.IsDirty = false;
                    allData.Add(projectType);
                }
                allData.Dirty = false;
            }
            return allData;
        }

        private PlanProjectPropertyCollection GetAllProjectProperties()
        {
            var queryStringBuilder = new StringBuilder();
            queryStringBuilder.AppendFormat("SELECT {0},{1},{2},{3} FROM {4}",
            ProjectPropertyIdColumnName,
            NameColumnName,
            ValueTypeColumnName,
            InSnapshotColumnName,
            ProjectPropertiesTableName);

            var allData = new PlanProjectPropertyCollection();
            using (SQLiteDataReader reader = SQLiteHelper.ExecuteSQLiteReader(queryStringBuilder.ToString(), _SqliteConnection))
            {
                while (reader.Read())
                {
                    var projectPropertyId = reader.GetInt32(0);
                    var name = reader.GetString(1);
                    var valueType = (ProjectPropertyValueType)reader.GetInt32(2);
                    var inSnapshot = reader.GetBoolean(3);

                    var property = new PlanProjectProperty(name, valueType, inSnapshot);
                    property.Id = projectPropertyId;
                    property.IsDirty = false;
                    allData.Add(property);
                }
            }
            allData.Dirty = false;
            return allData;
        }

        public List<Project> GetProjects()
        {
            var projectTypes = GetAllProjectTypes();
            var projectProperties = GetAllProjectProperties();

            _Projects = new List<Project>();

            var stringCache = new StringPool();
            var projectsQueryStringBuilder = new StringBuilder();
            projectsQueryStringBuilder.AppendFormat(
                @"SELECT {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},
                {15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},
                {30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41} FROM {42}",
                ProjectIdColumnName,
                DataSourceIdColumnName,
                IsFromDataSourceGroupColumnName,
                NameColumnName,
                OrderIdColumnName,
                ProjectDataFolderNameColumnName,
                ProjectTypeIdColumnName,
                IncludeColumnName,
                IncludeOriginalColumnName,
                ShiftValueColumnName,
                ShiftMinColumnName,
                ShiftMaxColumnName,
                ShiftOriginalColumnName,
                WiValueColumnName,
                WiMinColumnName,
                WiMaxColumnName,
                WiOriginalColumnName,
                PosValueColumnName,
                PosMinColumnName,
                PosMaxColumnName,
                PosOriginalColumnName,
                StartYearColumnName,
                DurationColumnName,
                PartnerNameColumnName,
                CustomColorArgbColumnName,
                EconomicLimitColumnName,
                MutuallyExclusiveGroupColumnName,
                IsSurrogateColumnName,
                SurrogateStepSizeColumnName,
                SurrogateMinTotalColumnName,
                SurrogateMaxTotalColumnName,
                IncludeDependencyColumnName,
                ShiftDependencyColumnName,
                ShiftOptionColumnName,
                ShiftValue1ColumnName,
                ShiftValue2ColumnName,
                ShiftParentDateOptionColumnName,
                ParentProjectIdColumnName,
                IsChildIndependentColumnName,
                WiDependencyColumnName,
                PosDependencyColumnName,
                DependencyDisplayColorColumnName,
                ProjectsTableName);

            var propertyValuesQueryBuilder = new StringBuilder();
            propertyValuesQueryBuilder.AppendFormat("SELECT {0},{1},{2} FROM {3}",
                            ProjectPropertyIdColumnName,
                            ProjectIdColumnName,
                            ValueColumnName,
                            PropertyValuesTableName);

            using (SQLiteDataReader reader = SQLiteHelper.ExecuteSQLiteReader(projectsQueryStringBuilder.ToString(), _SqliteConnection))
            {
                while (reader.Read())
                {
                    int index = 0;
                    var projectId = reader.GetInt32(index++);
                    var dataSourceId = reader.GetInt32(index++);
                    var isFromDataSourceGroup = reader.GetBoolean(index++);
                    var name = reader.GetString(index++);
                    var orderId = reader.GetInt32(index++);
                    var projectDataFolderName = reader.GetString(index++);
                    var projectTypeId = reader.GetInt32(index++);
                    var include = reader.GetBoolean(index++);
                    var includeOriginal = reader.GetBoolean(index++);
                    var shiftValue = reader.GetInt32(index++);
                    var shiftMin = reader.GetInt32(index++);
                    var shiftMax = reader.GetInt32(index++);
                    var shiftOriginal = reader.GetInt32(index++);
                    var wiValue = reader.IsDBNull(index) ? double.NaN : reader.GetDouble(index);
                    index++;
                    var wiMin = reader.IsDBNull(index) ? double.NaN : reader.GetDouble(index);
                    index++;
                    var wiMax = reader.IsDBNull(index) ? double.NaN : reader.GetDouble(index);
                    index++;
                    var wiOriginal = reader.IsDBNull(index) ? double.NaN : reader.GetDouble(index);
                    index++;
                    var posValue = reader.IsDBNull(index) ? double.NaN : reader.GetDouble(index);
                    index++;
                    var posMin = reader.IsDBNull(index) ? double.NaN : reader.GetDouble(index);
                    index++;
                    var posMax = reader.IsDBNull(index) ? double.NaN : reader.GetDouble(index);
                    index++;
                    var posOriginal = reader.IsDBNull(index) ? double.NaN : reader.GetDouble(index);
                    index++;
                    var startYear = reader.GetInt32(index++);
                    var duration = reader.GetInt32(index++);
                    var partnerName = stringCache.GetUnique(reader.GetString(index++));
                    var customColor = Color.FromArgb(reader.GetInt32(index++));
                    var economicLimit = reader.GetString(index++);
                    var mutuallyExclusiveGroup = reader.GetString(index++);

                    //project economic limit can be null. If it is null the economic limit won't be saved and the xml attribute 
                    //doesn't exist.
                    YearMonth economicLimitDate = string.IsNullOrEmpty(economicLimit) ? null : (YearMonth)economicLimit;
                    var planProjectType = projectTypes.GetById(projectTypeId) ?? projectTypes[0];

                    _Projects.Add(new Project {Id = projectId, Name=name, StartYear=startYear, Duration = duration, Properties = new List<Property>()});
                }
            }

            using (SQLiteDataReader reader = SQLiteHelper.ExecuteSQLiteReader(propertyValuesQueryBuilder.ToString(), _SqliteConnection))
            {
                while (reader.Read())
                {
                    var projectPropertyId = reader.GetInt32(0);
                    PlanProjectProperty projectProperty = projectProperties.GetById(projectPropertyId);
                    if (projectProperty == null)
                    {
                        continue;
                    }
                    var projectId = reader.GetInt32(1);
                    var project = _Projects.FirstOrDefault(p => p.Id == projectId);
                    var value = stringCache.GetUnique(reader.GetString(2));
                    var property = new Property() {Id = projectPropertyId, Name = projectProperty.Name, Value = value};
                    project.Properties.Add(property);
                }
            }

            return _Projects;
        }

        public List<Property> GetProperties(int projectId)
        {
            var project = _Projects.FirstOrDefault(p => p.Id == projectId);

            return project.Properties;
        }
        
    }
}