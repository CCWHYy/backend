import { ArchiveOrdersStack } from "./screens/ArchiveOrders";
import DrawerContent from "./components/DrawerContent";
import { NavigationContainer } from "@react-navigation/native";
import React from "react";
import { ReadyOrdersStack } from "./screens/ReadyOrders";
import { OrdersStack } from "./screens/Orders";
import { createDrawerNavigator } from "@react-navigation/drawer";

const Drawer = createDrawerNavigator();

export const Router = () => {
  return (
    <>
      <NavigationContainer>
        <Drawer.Navigator
          drawerContent={props => <DrawerContent {...props} />}
        >
          <Drawer.Screen name="OrdersStack" component={OrdersStack} />
          {/* <Drawer.Screen */}
          {/*   name="ArchiveOrdersStack" */}
          {/*   component={ArchiveOrdersStack} */}
          {/* /> */}
          {/* <Drawer.Screen */}
          {/*   name="ReadyOrdersStack" */}
          {/*   component={ReadyOrdersStack} */}
          {/* /> */}
        </Drawer.Navigator>
      </NavigationContainer>
    </>
  );
};
