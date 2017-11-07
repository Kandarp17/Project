package com.example.patel_000.myproject;

import android.app.Fragment;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v4.app.FragmentManager;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;

public class MainActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        SiteDetailFragment d=new SiteDetailFragment();
        FragmentManager mr=getSupportFragmentManager();
        getSupportActionBar().setTitle("Site Details");
        mr.beginTransaction().replace(R.id.content_main,d).commit();



        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_sitedetails) {
            SiteDetailFragment details=new SiteDetailFragment();
            FragmentManager manager=getSupportFragmentManager();
            getSupportActionBar().setTitle("Site Details");

            manager.beginTransaction().replace(R.id.content_main,details).commit();

        } else if (id == R.id.nav_sitedoc) {
            SiteDocumentFragment doc=new SiteDocumentFragment();
            FragmentManager manager=getSupportFragmentManager();
            getSupportActionBar().setTitle("Site Documents");
            manager.beginTransaction().replace(R.id.content_main,doc).commit();

        } else if (id == R.id.nav_sitenewphoto) {

            getSupportActionBar().setTitle("New Photo");
            Intent i = new Intent(MainActivity.this,PhotoUpdate.class);
            startActivity(i);

        } else if (id == R.id.nav_sitenewupdate) {
            UpdateFragment Fragment=new UpdateFragment();
            FragmentManager manager=getSupportFragmentManager();
            getSupportActionBar().setTitle("New Update");

            manager.beginTransaction().replace(R.id.content_main,Fragment).commit();

        } else if (id == R.id.nav_sitestaff) {
            StaffListFragment Fragment=new StaffListFragment();
            FragmentManager manager=getSupportFragmentManager();
            getSupportActionBar().setTitle("Site Staff");

            manager.beginTransaction().replace(R.id.content_main,Fragment).commit();

        } else if (id == R.id.nav_myissues) {
            IssueListFragment myissue =new IssueListFragment();
            FragmentManager manager=getSupportFragmentManager();
            getSupportActionBar().setTitle("My Issues");
            manager.beginTransaction().replace(R.id.content_main,myissue).commit();

        } else if (id == R.id.nav_newissue) {
            NewIssueFragment newissue =new NewIssueFragment();
            FragmentManager manager=getSupportFragmentManager();
            getSupportActionBar().setTitle("New Issue");
            manager.beginTransaction().replace(R.id.content_main,newissue).commit();


        } else if (id == R.id.nav_newbooking) {

            NewBookingFragment newbook =new NewBookingFragment();
            FragmentManager manager=getSupportFragmentManager();
            getSupportActionBar().setTitle("New Booking");
            manager.beginTransaction().replace(R.id.content_main,newbook).commit();

        } else if (id == R.id.nav_mybookings) {

            MyBookingFragment mybook =new MyBookingFragment();
            Bundle bundle = new Bundle();
            bundle.putString("state","MyBookings");
            mybook.setArguments(bundle);
            FragmentManager manager=getSupportFragmentManager();
            getSupportActionBar().setTitle("New Booking");
            manager.beginTransaction().replace(R.id.content_main,mybook).commit();

        }else if (id == R.id.nav_bookingoutstanding) {

            MyBookingFragment mybook =new MyBookingFragment();
            Bundle bundle = new Bundle();
            bundle.putString("state","PastBookings");
            mybook.setArguments(bundle);
            FragmentManager manager=getSupportFragmentManager();
            getSupportActionBar().setTitle("Outstanding Bookings");
            manager.beginTransaction().replace(R.id.content_main,mybook).commit();

        } else if (id == R.id.nav_logout) {
            SharedPreferences sp1=getSharedPreferences("Login",0);
            SharedPreferences.Editor editor = sp1.edit();
            editor.remove("Unm");
            editor.remove("Psw");
            editor.apply();
            Intent i = new Intent(MainActivity.this,Login.class);
            startActivity(i);

        }else if(id == R.id.nav_myprofile){
            MyProfileFragment myprofile=new MyProfileFragment();
            FragmentManager manager=getSupportFragmentManager();
            getSupportActionBar().setTitle("My Profile");

            manager.beginTransaction().replace(R.id.content_main,myprofile).commit();

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }


}
