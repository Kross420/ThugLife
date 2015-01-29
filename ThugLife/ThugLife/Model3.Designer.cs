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
namespace ThugLife
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class ThugLifeDBEntities4 : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new ThugLifeDBEntities4 object using the connection string found in the 'ThugLifeDBEntities4' section of the application configuration file.
        /// </summary>
        public ThugLifeDBEntities4() : base("name=ThugLifeDBEntities4", "ThugLifeDBEntities4")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ThugLifeDBEntities4 object.
        /// </summary>
        public ThugLifeDBEntities4(string connectionString) : base(connectionString, "ThugLifeDBEntities4")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new ThugLifeDBEntities4 object.
        /// </summary>
        public ThugLifeDBEntities4(EntityConnection connection) : base(connection, "ThugLifeDBEntities4")
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
        public ObjectSet<Player> Player
        {
            get
            {
                if ((_Player == null))
                {
                    _Player = base.CreateObjectSet<Player>("Player");
                }
                return _Player;
            }
        }
        private ObjectSet<Player> _Player;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Score> Score
        {
            get
            {
                if ((_Score == null))
                {
                    _Score = base.CreateObjectSet<Score>("Score");
                }
                return _Score;
            }
        }
        private ObjectSet<Score> _Score;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Player EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToPlayer(Player player)
        {
            base.AddObject("Player", player);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Score EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToScore(Score score)
        {
            base.AddObject("Score", score);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ThugLifeDBModel", Name="Player")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Player : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Player object.
        /// </summary>
        /// <param name="iD_Player">Initial value of the ID_Player property.</param>
        public static Player CreatePlayer(global::System.Int32 iD_Player)
        {
            Player player = new Player();
            player.ID_Player = iD_Player;
            return player;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID_Player
        {
            get
            {
                return _ID_Player;
            }
            set
            {
                if (_ID_Player != value)
                {
                    OnID_PlayerChanging(value);
                    ReportPropertyChanging("ID_Player");
                    _ID_Player = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID_Player");
                    OnID_PlayerChanged();
                }
            }
        }
        private global::System.Int32 _ID_Player;
        partial void OnID_PlayerChanging(global::System.Int32 value);
        partial void OnID_PlayerChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Surname
        {
            get
            {
                return _Surname;
            }
            set
            {
                OnSurnameChanging(value);
                ReportPropertyChanging("Surname");
                _Surname = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Surname");
                OnSurnameChanged();
            }
        }
        private global::System.String _Surname;
        partial void OnSurnameChanging(global::System.String value);
        partial void OnSurnameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Username
        {
            get
            {
                return _Username;
            }
            set
            {
                OnUsernameChanging(value);
                ReportPropertyChanging("Username");
                _Username = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Username");
                OnUsernameChanged();
            }
        }
        private global::System.String _Username;
        partial void OnUsernameChanging(global::System.String value);
        partial void OnUsernameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Password
        {
            get
            {
                return _Password;
            }
            set
            {
                OnPasswordChanging(value);
                ReportPropertyChanging("Password");
                _Password = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Password");
                OnPasswordChanged();
            }
        }
        private global::System.String _Password;
        partial void OnPasswordChanging(global::System.String value);
        partial void OnPasswordChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Email
        {
            get
            {
                return _Email;
            }
            set
            {
                OnEmailChanging(value);
                ReportPropertyChanging("Email");
                _Email = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Email");
                OnEmailChanged();
            }
        }
        private global::System.String _Email;
        partial void OnEmailChanging(global::System.String value);
        partial void OnEmailChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="ThugLifeDBModel", Name="Score")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Score : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Score object.
        /// </summary>
        /// <param name="iD_Score">Initial value of the ID_Score property.</param>
        public static Score CreateScore(global::System.Int32 iD_Score)
        {
            Score score = new Score();
            score.ID_Score = iD_Score;
            return score;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID_Score
        {
            get
            {
                return _ID_Score;
            }
            set
            {
                if (_ID_Score != value)
                {
                    OnID_ScoreChanging(value);
                    ReportPropertyChanging("ID_Score");
                    _ID_Score = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID_Score");
                    OnID_ScoreChanged();
                }
            }
        }
        private global::System.Int32 _ID_Score;
        partial void OnID_ScoreChanging(global::System.Int32 value);
        partial void OnID_ScoreChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> ID_Player
        {
            get
            {
                return _ID_Player;
            }
            set
            {
                OnID_PlayerChanging(value);
                ReportPropertyChanging("ID_Player");
                _ID_Player = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ID_Player");
                OnID_PlayerChanged();
            }
        }
        private Nullable<global::System.Int32> _ID_Player;
        partial void OnID_PlayerChanging(Nullable<global::System.Int32> value);
        partial void OnID_PlayerChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> GameScore
        {
            get
            {
                return _GameScore;
            }
            set
            {
                OnGameScoreChanging(value);
                ReportPropertyChanging("GameScore");
                _GameScore = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("GameScore");
                OnGameScoreChanged();
            }
        }
        private Nullable<global::System.Int32> _GameScore;
        partial void OnGameScoreChanging(Nullable<global::System.Int32> value);
        partial void OnGameScoreChanged();

        #endregion

    
    }

    #endregion

    
}
