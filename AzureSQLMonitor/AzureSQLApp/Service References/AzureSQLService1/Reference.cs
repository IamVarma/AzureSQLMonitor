﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 12.0.21005.1
// 
namespace AzureSQLApp.AzureSQLService1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AzureSQLService1.ISqlMonitorService")]
    public interface ISqlMonitorService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISqlMonitorService/ConnectSQLAzure", ReplyAction="http://tempuri.org/ISqlMonitorService/ConnectSQLAzureResponse")]
        System.Threading.Tasks.Task<string> ConnectSQLAzureAsync(string servername, string loginName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISqlMonitorService/GetDatabaseList", ReplyAction="http://tempuri.org/ISqlMonitorService/GetDatabaseListResponse")]
        System.Threading.Tasks.Task<string> GetDatabaseListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISqlMonitorService/getSystemInfo", ReplyAction="http://tempuri.org/ISqlMonitorService/getSystemInfoResponse")]
        System.Threading.Tasks.Task<string> getSystemInfoAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISqlMonitorService/GetDatabaseSize", ReplyAction="http://tempuri.org/ISqlMonitorService/GetDatabaseSizeResponse")]
        System.Threading.Tasks.Task<string> GetDatabaseSizeAsync(string dbname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISqlMonitorService/GetConnectionCount", ReplyAction="http://tempuri.org/ISqlMonitorService/GetConnectionCountResponse")]
        System.Threading.Tasks.Task<string> GetConnectionCountAsync(string dbname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISqlMonitorService/GetTableSizeDetails", ReplyAction="http://tempuri.org/ISqlMonitorService/GetTableSizeDetailsResponse")]
        System.Threading.Tasks.Task<string> GetTableSizeDetailsAsync(string dbname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISqlMonitorService/GetDBResourceUsage", ReplyAction="http://tempuri.org/ISqlMonitorService/GetDBResourceUsageResponse")]
        System.Threading.Tasks.Task<string> GetDBResourceUsageAsync(string dbname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISqlMonitorService/GetDBConnectionDetails", ReplyAction="http://tempuri.org/ISqlMonitorService/GetDBConnectionDetailsResponse")]
        System.Threading.Tasks.Task<string> GetDBConnectionDetailsAsync(string dbname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISqlMonitorService/GetTopCpuConsumers", ReplyAction="http://tempuri.org/ISqlMonitorService/GetTopCpuConsumersResponse")]
        System.Threading.Tasks.Task<string> GetTopCpuConsumersAsync(string dbname);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISqlMonitorServiceChannel : global::AzureSQLApp.AzureSQLService1.ISqlMonitorService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SqlMonitorServiceClient : System.ServiceModel.ClientBase<global::AzureSQLApp.AzureSQLService1.ISqlMonitorService>, global::AzureSQLApp.AzureSQLService1.ISqlMonitorService {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SqlMonitorServiceClient() : 
                base(SqlMonitorServiceClient.GetDefaultBinding(), SqlMonitorServiceClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ISqlMonitorService.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SqlMonitorServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(SqlMonitorServiceClient.GetBindingForEndpoint(endpointConfiguration), SqlMonitorServiceClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SqlMonitorServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SqlMonitorServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SqlMonitorServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SqlMonitorServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SqlMonitorServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<string> ConnectSQLAzureAsync(string servername, string loginName, string password) {
            return base.Channel.ConnectSQLAzureAsync(servername, loginName, password);
        }
        
        public System.Threading.Tasks.Task<string> GetDatabaseListAsync() {
            return base.Channel.GetDatabaseListAsync();
        }
        
        public System.Threading.Tasks.Task<string> getSystemInfoAsync() {
            return base.Channel.getSystemInfoAsync();
        }
        
        public System.Threading.Tasks.Task<string> GetDatabaseSizeAsync(string dbname) {
            return base.Channel.GetDatabaseSizeAsync(dbname);
        }
        
        public System.Threading.Tasks.Task<string> GetConnectionCountAsync(string dbname) {
            return base.Channel.GetConnectionCountAsync(dbname);
        }
        
        public System.Threading.Tasks.Task<string> GetTableSizeDetailsAsync(string dbname) {
            return base.Channel.GetTableSizeDetailsAsync(dbname);
        }
        
        public System.Threading.Tasks.Task<string> GetDBResourceUsageAsync(string dbname) {
            return base.Channel.GetDBResourceUsageAsync(dbname);
        }
        
        public System.Threading.Tasks.Task<string> GetDBConnectionDetailsAsync(string dbname) {
            return base.Channel.GetDBConnectionDetailsAsync(dbname);
        }
        
        public System.Threading.Tasks.Task<string> GetTopCpuConsumersAsync(string dbname) {
            return base.Channel.GetTopCpuConsumersAsync(dbname);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISqlMonitorService)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISqlMonitorService)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:60693/SqlMonitorService.svc");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return SqlMonitorServiceClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ISqlMonitorService);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return SqlMonitorServiceClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ISqlMonitorService);
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_ISqlMonitorService,
        }
    }
}
