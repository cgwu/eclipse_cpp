﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompanyServiceConsoleUser.CompanyService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CompanyService.ICompanyService")]
    public interface ICompanyService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICompanyService/GetInformation", ReplyAction="http://tempuri.org/ICompanyService/GetInformationResponse")]
        string GetInformation();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICompanyServiceChannel : CompanyServiceConsoleUser.CompanyService.ICompanyService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CompanyServiceClient : System.ServiceModel.ClientBase<CompanyServiceConsoleUser.CompanyService.ICompanyService>, CompanyServiceConsoleUser.CompanyService.ICompanyService {
        
        public CompanyServiceClient() {
        }
        
        public CompanyServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CompanyServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CompanyServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CompanyServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetInformation() {
            return base.Channel.GetInformation();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CompanyService.ICompanyConfidentialService")]
    public interface ICompanyConfidentialService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICompanyConfidentialService/GetConfidentialInformation", ReplyAction="http://tempuri.org/ICompanyConfidentialService/GetConfidentialInformationResponse" +
            "")]
        string GetConfidentialInformation();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICompanyConfidentialServiceChannel : CompanyServiceConsoleUser.CompanyService.ICompanyConfidentialService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CompanyConfidentialServiceClient : System.ServiceModel.ClientBase<CompanyServiceConsoleUser.CompanyService.ICompanyConfidentialService>, CompanyServiceConsoleUser.CompanyService.ICompanyConfidentialService {
        
        public CompanyConfidentialServiceClient() {
        }
        
        public CompanyConfidentialServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CompanyConfidentialServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CompanyConfidentialServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CompanyConfidentialServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetConfidentialInformation() {
            return base.Channel.GetConfidentialInformation();
        }
    }
}
