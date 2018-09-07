//
//  UJSInterface.m
//  Unity-iPhone
//
//  Created by MacMini on 15-11-20.
//
//
#import "IAPInterface.h"
//#import "ViewController.h"
#import "IAPManager.h"


@implementation IAPInterface


void TestMsg(){
    NSLog(@"Msg received");
    
    
}
void TestGameCenter()
{
    }


void TestSendString(void *p){
    NSString *list = [NSString stringWithUTF8String:p];
    NSArray *listItems = [list componentsSeparatedByString:@"\t"];
    
    for (int i =0; i<listItems.count; i++) {
        NSLog(@"msg %d : %@",i,listItems[i]);
    }
    
}

void TestGetString(){
    NSArray *test = [NSArray arrayWithObjects:@"t1",@"t2",@"t3", nil];
    NSString *join = [test componentsJoinedByString:@"\n"];
    
    
    UnitySendMessage("IOSBackMsg", "IOSToU", [join UTF8String]);
}


IAPManager *iapManager = nil;


void InitIAPManager(){
   
    NSLog(@"---初始化app内购-----");

    iapManager = [[IAPManager alloc] init];
    [iapManager attachObserver];
  
}

bool IsProductAvailable(){
    return [iapManager CanMakePayment];
}


void RequstProductInfo(void *p){
    NSLog(@"--------2");

    NSString *list = [NSString stringWithUTF8String:p];
    NSLog(@"productKey:%@",list);
    [iapManager requestProductData:list];
    
}


void BuyProduct(void *p){
    NSLog(@"--------5");
    [iapManager buyRequest:[NSString stringWithUTF8String:p]];
}
void WXShareAction()
{
    
}

@end