﻿#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <stdint.h>
#include <limits>



// System.Void
struct Void_t700C6383A2A510C2CF4DD86DABD5CA9FF70ADAC5;



IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// System.Object


// System.ValueType
struct ValueType_tDBF999C1B75C48C68621878250DBF6CDBCF51E52  : public RuntimeObject
{
public:

public:
};

// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_tDBF999C1B75C48C68621878250DBF6CDBCF51E52_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_tDBF999C1B75C48C68621878250DBF6CDBCF51E52_marshaled_com
{
};

// System.IntPtr
struct IntPtr_t 
{
public:
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;

public:
	inline static int32_t get_offset_of_m_value_0() { return static_cast<int32_t>(offsetof(IntPtr_t, ___m_value_0)); }
	inline void* get_m_value_0() const { return ___m_value_0; }
	inline void** get_address_of_m_value_0() { return &___m_value_0; }
	inline void set_m_value_0(void* value)
	{
		___m_value_0 = value;
	}
};


// System.Void
struct Void_t700C6383A2A510C2CF4DD86DABD5CA9FF70ADAC5 
{
public:
	union
	{
		struct
		{
		};
		uint8_t Void_t700C6383A2A510C2CF4DD86DABD5CA9FF70ADAC5__padding[1];
	};

public:
};


// System.Object


// System.Object


// System.ValueType


// System.ValueType


// System.IntPtr

struct IntPtr_t_StaticFields
{
public:
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;

public:
	inline static int32_t get_offset_of_Zero_1() { return static_cast<int32_t>(offsetof(IntPtr_t_StaticFields, ___Zero_1)); }
	inline intptr_t get_Zero_1() const { return ___Zero_1; }
	inline intptr_t* get_address_of_Zero_1() { return &___Zero_1; }
	inline void set_Zero_1(intptr_t value)
	{
		___Zero_1 = value;
	}
};


// System.IntPtr


// System.Void


// System.Void

#ifdef __clang__
#pragma clang diagnostic pop
#endif

extern "C" char* DEFAULT_CALL ReversePInvokeWrapper_ARFoundationAnchorManager_OnCreateAnchor_m219CD5EC6B36FAC7D9231ED55004BD1803C58162(intptr_t ___posePtr0);
extern "C" int32_t DEFAULT_CALL ReversePInvokeWrapper_ARFoundationAnchorManager_OnRemoveAnchor_m53E03B2AFB27557FB362FE455CFB0305B80F16D8(intptr_t ___uuidPtr0);
extern "C" void DEFAULT_CALL ReversePInvokeWrapper_OSSpecificSynchronizationContext_InvocationEntry_m4EF49088C9F8BEC14A9AD73F1C0F992FE808B3E9(intptr_t ___arg0);


IL2CPP_EXTERN_C const Il2CppMethodPointer g_ReversePInvokeWrapperPointers[];
const Il2CppMethodPointer g_ReversePInvokeWrapperPointers[3] = 
{
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_ARFoundationAnchorManager_OnCreateAnchor_m219CD5EC6B36FAC7D9231ED55004BD1803C58162),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_ARFoundationAnchorManager_OnRemoveAnchor_m53E03B2AFB27557FB362FE455CFB0305B80F16D8),
	reinterpret_cast<Il2CppMethodPointer>(ReversePInvokeWrapper_OSSpecificSynchronizationContext_InvocationEntry_m4EF49088C9F8BEC14A9AD73F1C0F992FE808B3E9),
};
