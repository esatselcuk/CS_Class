﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GH_CS_Class
{
    class clsWMI_Enum
    {
        enum win32_Types
        {
            Win32_Processor,
            Win32_VideoController,
            Win32_OperatingSystem,
            Win32_SoundDevice
        }
        enum Win32_Processor
        {
            //https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-processor adresinden aşağıdaki listeyi bulabilirsiniz.
            AddressWidth,
            Architecture,
            AssetTag,
            Availability,
            Caption,
            Characteristics,
            ConfigManagerErrorCode,
            ConfigManagerUserConfig,
            CpuStatus,
            CreationClassName,
            CurrentClockSpeed,
            CurrentVoltage,
            DataWidth,
            Description,
            DeviceID,
            ErrorCleared,
            ErrorDescription,
            ExtClock,
            Family,
            InstallDate,
            L2CacheSize,
            L2CacheSpeed,
            L3CacheSize,
            L3CacheSpeed,
            LastErrorCode,
            Level,
            LoadPercentage,
            Manufacturer,
            MaxClockSpeed,
            Name,
            NumberOfCores,
            NumberOfEnabledCore,
            NumberOfLogicalProcessors,
            OtherFamilyDescription,
            PartNumber,
            PNPDeviceID,
            PowerManagementCapabilities,
            PowerManagementSupported,
            ProcessorId,
            ProcessorType,
            Revision,
            Role,
            SecondLevelAddressTranslationExtensions,
            SerialNumber,
            SocketDesignation,
            Status,
            StatusInfo,
            Stepping,
            SystemCreationClassName,
            SystemName,
            ThreadCount,
            UniqueId,
            UpgradeMethod,
            Version,
            VirtualizationFirmwareEnabled,
            VMMonitorModeExtensions,
            VoltageCaps
        }

        enum Win32_VideoController
        {
            //https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-videocontroller adresinden aşağıdaki listeyi bulabilirsiniz.
            AcceleratorCapabilities,
            AdapterCompatibility,
            AdapterDACType,
            AdapterRAM,
            Availability,
            CapabilityDescriptions,
            Caption,
            ColorTableEntries,
            ConfigManagerErrorCode,
            ConfigManagerUserConfig,
            CreationClassName,
            CurrentBitsPerPixel,
            CurrentHorizontalResolution,
            CurrentNumberOfColors,
            CurrentNumberOfColumns,
            CurrentNumberOfRows,
            CurrentRefreshRate,
            CurrentScanMode,
            CurrentVerticalResolution,
            Description,
            DeviceID,
            DeviceSpecificPens,
            DitherType,
            DriverDate,
            DriverVersion,
            ErrorCleared,
            ErrorDescription,
            ICMIntent,
            ICMMethod,
            InfFilename,
            InfSection,
            InstallDate,
            InstalledDisplayDrivers,
            LastErrorCode,
            MaxMemorySupported,
            MaxNumberControlled,
            MaxRefreshRate,
            MinRefreshRate,
            Monochrome,
            Name,
            NumberOfColorPlanes,
            NumberOfVideoPages,
            PNPDeviceID,
            PowerManagementCapabilities,
            PowerManagementSupported,
            ProtocolSupported,
            ReservedSystemPaletteEntries,
            SpecificationVersion,
            Status,
            StatusInfo,
            SystemCreationClassName,
            SystemName,
            SystemPaletteEntries,
            TimeOfLastReset,
            VideoArchitecture,
            VideoMemoryType,
            VideoMode
        }

        enum Win32_OperatingSystem
        {
            //https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-operatingsystem adresinden aşağıdaki listeyi bulabilirsiniz.
            BootDevice,
            BuildNumber,
            BuildType,
            Caption,
            CodeSet,
            CountryCode,
            CreationClassName,
            CSCreationClassName,
            CSDVersion,
            CSName,
            CurrentTimeZone,
            DataExecutionPrevention_Available,
            DataExecutionPrevention_32BitApplications,
            DataExecutionPrevention_Drivers,
            DataExecutionPrevention_SupportPolicy,
            Debug,
            Description,
            Distributed,
            EncryptionLevel,
            ForegroundApplicationBoost = 2,
            FreePhysicalMemory,
            FreeSpaceInPagingFiles,
            FreeVirtualMemory,
            InstallDate,
            LargeSystemCache,
            LastBootUpTime,
            LocalDateTime,
            Locale,
            Manufacturer,
            MaxNumberOfProcesses,
            MaxProcessMemorySize,
            MUILanguages,
            Name,
            NumberOfLicensedUsers,
            NumberOfProcesses,
            NumberOfUsers,
            OperatingSystemSKU,
            Organization,
            OSArchitecture,
            OSLanguage,
            OSProductSuite,
            OSType,
            OtherTypeDescription,
            PAEEnabled,
            PlusProductID,
            PlusVersionNumber,
            PortableOperatingSystem,
            Primary,
            ProductType,
            RegisteredUser,
            SerialNumber,
            ServicePackMajorVersion,
            ServicePackMinorVersion,
            SizeStoredInPagingFiles,
            Status,
            SuiteMask,
            SystemDevice,
            SystemDirectory,
            SystemDrive,
            TotalSwapSpaceSize,
            TotalVirtualMemorySize,
            TotalVisibleMemorySize,
            Version,
            WindowsDirectory,
            QuantumLength,
            QuantumType,

        }
        enum Win32_SoundDevice
        {
            //https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-sounddevice adresinden aşağıdaki listeyi bulabilirsiniz.
            Availability,
            Caption,
            ConfigManagerErrorCode,
            ConfigManagerUserConfig,
            CreationClassName,
            Description,
            DeviceID,
            DMABufferSize,
            ErrorCleared,
            ErrorDescription,
            InstallDate,
            LastErrorCode,
            Manufacturer,
            MPU401Address,
            Name,
            PNPDeviceID,
            PowerManagementCapabilities,
            PowerManagementSupported,
            ProductName,
            Status,
            StatusInfo,
            SystemCreationClassName,
            SystemName
        }

        static readonly string[] Boyut_Cinsleri = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };    
        static string Boyut_Cinsi(Int64 value)
        {
            //Parametre Olarak Gelen Değeri KB, MB, vb cinsinden gösterecektir.
            if (value < 0) { return "-" + Boyut_Cinsi(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, Boyut_Cinsleri[mag]);
        }
    }
}
