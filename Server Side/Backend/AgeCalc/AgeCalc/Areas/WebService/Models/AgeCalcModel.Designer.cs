﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace AgeCalc.Areas.WebService.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class AgeCalcEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new AgeCalcEntities object using the connection string found in the 'AgeCalcEntities' section of the application configuration file.
        /// </summary>
        public AgeCalcEntities() : base("name=AgeCalcEntities", "AgeCalcEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new AgeCalcEntities object.
        /// </summary>
        public AgeCalcEntities(string connectionString) : base(connectionString, "AgeCalcEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new AgeCalcEntities object.
        /// </summary>
        public AgeCalcEntities(EntityConnection connection) : base(connection, "AgeCalcEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<UserAge> UserAges
        {
            get
            {
                if ((_UserAges == null))
                {
                    _UserAges = base.CreateObjectSet<UserAge>("UserAges");
                }
                return _UserAges;
            }
        }
        private ObjectSet<UserAge> _UserAges;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the UserAges EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToUserAges(UserAge userAge)
        {
            base.AddObject("UserAges", userAge);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="AgeCalcModel", Name="UserAge")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class UserAge : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new UserAge object.
        /// </summary>
        /// <param name="userId">Initial value of the UserId property.</param>
        /// <param name="firstName">Initial value of the FirstName property.</param>
        /// <param name="lastName">Initial value of the LastName property.</param>
        /// <param name="dateOfBirth">Initial value of the DateOfBirth property.</param>
        public static UserAge CreateUserAge(global::System.Int64 userId, global::System.String firstName, global::System.String lastName, global::System.DateTime dateOfBirth)
        {
            UserAge userAge = new UserAge();
            userAge.UserId = userId;
            userAge.FirstName = firstName;
            userAge.LastName = lastName;
            userAge.DateOfBirth = dateOfBirth;
            return userAge;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int64 UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if (_UserId != value)
                {
                    OnUserIdChanging(value);
                    ReportPropertyChanging("UserId");
                    _UserId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("UserId");
                    OnUserIdChanged();
                }
            }
        }
        private global::System.Int64 _UserId;
        partial void OnUserIdChanging(global::System.Int64 value);
        partial void OnUserIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                OnFirstNameChanging(value);
                ReportPropertyChanging("FirstName");
                _FirstName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("FirstName");
                OnFirstNameChanged();
            }
        }
        private global::System.String _FirstName;
        partial void OnFirstNameChanging(global::System.String value);
        partial void OnFirstNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                OnLastNameChanging(value);
                ReportPropertyChanging("LastName");
                _LastName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("LastName");
                OnLastNameChanged();
            }
        }
        private global::System.String _LastName;
        partial void OnLastNameChanging(global::System.String value);
        partial void OnLastNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime DateOfBirth
        {
            get
            {
                return _DateOfBirth;
            }
            set
            {
                OnDateOfBirthChanging(value);
                ReportPropertyChanging("DateOfBirth");
                _DateOfBirth = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("DateOfBirth");
                OnDateOfBirthChanged();
            }
        }
        private global::System.DateTime _DateOfBirth;
        partial void OnDateOfBirthChanging(global::System.DateTime value);
        partial void OnDateOfBirthChanged();

        #endregion

        public string DateOfBirthString
        {
            get
            {
                return _DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
    
    }

    #endregion

    
}
