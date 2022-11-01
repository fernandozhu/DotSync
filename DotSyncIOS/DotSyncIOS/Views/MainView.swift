//
//  MainView.swift
//  DotSyncIOS
//
//  Created by Fernando Zhu on 1/11/22.
//

import SwiftUI

struct MainView: View {
    var body: some View {
        
        TabView {
            ContentView().tabItem {
                Label("Tab1", systemImage: "list.dash")
            }
            
        }
        
        
            Text("main")
    }
}

struct MainView_Previews: PreviewProvider {
    static var previews: some View {
        MainView()
    }
}
